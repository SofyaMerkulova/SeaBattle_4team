using GameData.Models;
using GameData.Repositories;
using GameForClients.Servies;

namespace GameForClients
{
    public partial class GameForm : Form
    {
        private readonly IGameRepository _gameRepo;
        private bool _isWaitingForOpponent;
        private readonly GameService _gameService;
        private readonly IShipRepository _shipRepo;
        private readonly int _currentPlayerId; // Правильное имя поля
        private int _currentGameId;
        private List<Ship> _playerShips = new List<Ship>();
        private List<ForCounterShips> _enemyShips = new List<ForCounterShips>();
        private HashSet<Point> _enemyHits = new HashSet<Point>();

        private enum GamePhase { Placement, Battle }
        private GamePhase _currentPhase = GamePhase.Placement;

        public GameForm(GameService gameService, IGameRepository gameRepo, int playerId, int? existingGameId = null)
        {
            _gameService = gameService;
            _currentPlayerId = playerId; 
            _gameRepo = gameRepo;
            _shipRepo = new ShipRepository(new DbForGame());
            InitializeComponent();
            InitializeGameAsync(existingGameId);
        }
        private void SetupShipPlacement()
        {
            _currentPhase = GamePhase.Placement;
            this.Text = "Расстановка кораблей";
            MessageBox.Show("Расставьте свои корабли на левом поле");

            // Активируем только свою доску
            foreach (Control c in panelPlayer.Controls)
            {
                if (c is Button btn)
                {
                    btn.Click -= PlaceShip_Click; // Удаляем старые обработчики
                    btn.Click += PlaceShip_Click;
                    btn.BackColor = Color.MidnightBlue;
                    btn.Enabled = true;
                }
            }

            foreach (Control c in panelEnemy.Controls)
            {
                if (c is Button btn) btn.Enabled = false;
            }
        }

        private async void InitializeGameAsync(int? existingGameId = null)
        {
            try
            {
                if (existingGameId.HasValue)
                {
                    // Режим присоединения к существующей игре
                    _currentGameId = existingGameId.Value;
                    if (!await _gameService.JoinGame(_currentGameId, _currentPlayerId))
                    {
                        MessageBox.Show("Не удалось присоединиться к игре");
                        this.Close();
                        return;
                    }

                    // Загружаем свои корабли
                    _playerShips = (await _shipRepo.GetByGameIdAsync(_currentGameId))
                        .Where(s => s.PlayerId == _currentPlayerId).ToList();

                    // Если кораблей нет - значит нужно расставить
                    if (_playerShips.Count == 0)
                    {
                        CreateGameBoards();
                        MessageBox.Show("Расставьте свои корабли на левом поле");
                        SetupShipPlacement();
                    }
                    else
                    {
                        // Если корабли уже есть - сразу начинаем бой
                        CreateGameBoards();
                        _currentPhase = GamePhase.Battle;
                        MessageBox.Show("Все корабли расставлены! Начинаем бой.");
                        EnableBattleMode();
                    }
                }
                else
                {
                    // Режим создания новой игры
                    _currentGameId = await _gameService.CreateGame(_currentPlayerId);
                    _playerShips = new List<Ship>();

                    CreateGameBoards();
                    MessageBox.Show("Расставьте свои корабли на левом поле");
                    SetupShipPlacement();

                    // Для multiplayer: ожидание второго игрока
                    _isWaitingForOpponent = true;
                    MessageBox.Show("Ожидаем второго игрока...");
                    StartOpponentWaitingTimer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации: {ex.Message}");
                this.Close();
            }
        }

        private void StartOpponentWaitingTimer()
        {
            var timer = new System.Windows.Forms.Timer { Interval = 1000 };
            timer.Tick += async (s, e) =>
            {
                var game = await _gameRepo.GetByIdAsync(_currentGameId);
                if (game.Player2Id != null)
                {
                    timer.Stop();
                    _isWaitingForOpponent = false;

                    // Проверяем, нужно ли нам расставлять корабли
                    if (_playerShips.Count == 0)
                    {
                        MessageBox.Show("Расставьте свои корабли");
                        MessageBox.Show("Второй игрок подключился! Расставьте корабли.");
                    }
                    else
                    {
                        // Если корабли уже расставлены - начинаем бой
                        _currentPhase = GamePhase.Battle;
                        EnableBattleMode();
                        MessageBox.Show("Второй игрок подключился! Начинаем бой.");
                    }
                }
            };
            timer.Start();
        }

        private void CreateGameBoards()
        {
            CreateBoard(panelPlayer, false);
            CreateBoard(panelEnemy, true);
        }

        private void CreateBoard(Panel panel, bool isEnemy)
        {
            panel.Controls.Clear();
            int cellSize = 30;

            // Буквы (A-К)
            for (int col = 0; col < 10; col++)
            {
                var label = new Label
                {
                    Text = ((char)('А' + col)).ToString(),
                    Size = new Size(cellSize, cellSize),
                    Location = new Point((col + 1) * cellSize, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White
                };
                panel.Controls.Add(label);
            }

            // Цифры (1-10)
            for (int row = 0; row < 10; row++)
            {
                var label = new Label
                {
                    Text = (row + 1).ToString(),
                    Size = new Size(cellSize, cellSize),
                    Location = new Point(0, (row + 1) * cellSize),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White
                };
                panel.Controls.Add(label);
            }

            // Игровое поле
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var btn = new Button
                    {
                        Size = new Size(cellSize, cellSize),
                        Location = new Point((col + 1) * cellSize, (row + 1) * cellSize),
                        Tag = new Point(col, row),
                        BackColor = Color.MidnightBlue,
                        FlatStyle = FlatStyle.Flat
                    };

                    if (!isEnemy && _currentPhase == GamePhase.Placement)
                    {
                        btn.Click += PlaceShip_Click;
                    }
                    else if (isEnemy && _currentPhase == GamePhase.Battle)
                    {
                        btn.Click += AttackEnemy_Click;
                    }

                    panel.Controls.Add(btn);
                }
            }
        }

        private async void PlaceShip_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var pos = (Point)btn.Tag;

            if (_playerShips.Any(s => s.Cells.Contains($"{pos.X},{pos.Y}")))
            {
                MessageBox.Show("Здесь уже есть корабль!");
                return;
            }

            var ship = new Ship
            {
                GameId = _currentGameId,
                PlayerId = _currentPlayerId,
                ShipType = "Single",
                Cells = $"{pos.X},{pos.Y}"
            };

            await _shipRepo.AddAsync(ship);
            await _shipRepo.SaveChangesAsync();

            _playerShips.Add(ship);
            btn.BackColor = Color.White; // Отмечаем корабль серым цветом

            if (_playerShips.Count >= 20) // После размещения всех кораблей
            {
                _currentPhase = GamePhase.Battle;
                MessageBox.Show("Все корабли размещены! Начинаем бой.");
                EnableBattleMode();
            }
        }

        private void EnableBattleMode()
        {
            foreach (Control c in panelPlayer.Controls)
                if (c is Button btn) btn.Enabled = false;

            foreach (Control c in panelEnemy.Controls)
                if (c is Button btn) btn.Enabled = true;
        }

        private async void AttackEnemy_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var pos = (Point)btn.Tag;

            bool isHit = await _gameService.MakeMove(_currentGameId, _currentPlayerId, pos.X, pos.Y);

            btn.BackColor = isHit ? Color.Red : Color.White;
            btn.Enabled = false;

            if (isHit)
                MessageBox.Show("Попадание!");
            else
                MessageBox.Show("Мимо!");

            if (await CheckGameEnd())
            {
                MessageBox.Show("Вы победили!");
                this.Close();
            }
        }

        private async Task<bool> CheckGameEnd()
        {
            var enemyShips = (await _shipRepo.GetByGameIdAsync(_currentGameId))
                .Where(s => s.PlayerId != _currentPlayerId);

            foreach (var ship in enemyShips)
            {
                foreach (var cell in ship.Cells.Split(';'))
                {
                    var coords = cell.Split(',');
                    if (coords.Length != 2) continue;

                    int x = int.Parse(coords[0]);
                    int y = int.Parse(coords[1]);

                    if (!await _gameService.WasHit(_currentGameId, x, y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

       
    }
}