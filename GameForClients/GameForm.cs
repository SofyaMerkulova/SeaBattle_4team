using GameData.Models;
using GameData.Repositories;
using GameForClients.Servies;

namespace GameForClients
{
    /// <summary>  
    /// Форма игры и логика для панелей и кнопок
    /// </summary>
    public partial class GameForm : Form
    {
        private readonly IMoveRepository moveRepository;
        private readonly IGameRepository _gameRepo;
        private bool _isWaitingForOpponent;
        private readonly GameService _gameService;
        private readonly IShipRepository _shipRepo;
        private readonly int _currentPlayerId;
        private int _currentGameId;
        private List<Ship> _playerShips = new List<Ship>();
        private List<ForCounterShips> _enemyShips = new List<ForCounterShips>();
        private HashSet<Point> _enemyHits = new HashSet<Point>();
        private System.Windows.Forms.Timer _syncTimer;


        private enum GamePhase { Placement, Battle }
        private GamePhase _currentPhase = GamePhase.Placement;

        public GameForm(GameService gameService, IGameRepository gameRepo, IMoveRepository moveRepository, int playerId, int? existingGameId = null)
        {
            this.moveRepository = moveRepository;
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


            foreach (Control c in panelPlayer.Controls)
            {
                if (c is Button btn)
                {
                    btn.Click -= PlaceShip_Click;
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
                    _currentGameId = existingGameId.Value;
                    if (!await _gameService.JoinGame(_currentGameId, _currentPlayerId))
                    {
                        MessageBox.Show("Не удалось присоединиться к игре");
                        this.Close();
                        return;
                    }

                    _playerShips = (await _shipRepo.GetByGameIdAsync(_currentGameId))
                        .Where(s => s.PlayerId == _currentPlayerId).ToList();

                    if (_playerShips.Count == 0)
                    {
                        CreateGameBoards();
                        MessageBox.Show("Расставьте свои корабли на левом поле");
                        SetupShipPlacement();
                    }
                    else
                    {
                        CreateGameBoards();
                        _currentPhase = GamePhase.Battle;
                        MessageBox.Show("Все корабли расставлены! Начинаем бой.");
                        EnableBattleMode();
                    }
                }
                else
                {
                    _currentGameId = await _gameService.CreateGame(_currentPlayerId);
                    _playerShips = new List<Ship>();

                    CreateGameBoards();
                    MessageBox.Show("Расставьте свои корабли на левом поле");
                    SetupShipPlacement();

                    _isWaitingForOpponent = true;
                    MessageBox.Show("Ожидаем второго игрока...");
                    StartOpponentWaitingTimer();
                }
                _syncTimer = new System.Windows.Forms.Timer { Interval = 1000 };
                _syncTimer.Tick += SyncGameState;
                _syncTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации: {ex.Message}");
                this.Close();
            }
        }
      
        private async void SyncGameState(object sender, EventArgs e)
        {
            var allMoves = await moveRepository.GetByGameIdAsync(_currentGameId);
            var enemyMoves = allMoves.Where(m => m.PlayerId != _currentPlayerId);

            foreach (var move in enemyMoves)
            {
                var btn = panelPlayer.Controls
                    .OfType<Button>()
                    .FirstOrDefault(b => ((Point)b.Tag).X == move.X &&
                                       ((Point)b.Tag).Y == move.Y);

                if (btn != null)
                {
                    btn.BackColor = move.IsHit ? Color.Red : Color.White;
                }
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
                    if (_playerShips.Count == 0)
                    {
                        MessageBox.Show("Второй игрок подключился! Расставьте корабли.");
                    }
                    else
                    {

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
                    else if (isEnemy)
                    {
                        btn.Click -= AttackEnemy_Click;
                        if (_currentPhase == GamePhase.Battle)
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
            btn.BackColor = Color.White;

            if (_playerShips.Count >= 20)
            {
                _currentPhase = GamePhase.Battle;
                MessageBox.Show("Все корабли размещены! Начинаем бой.");
                EnableBattleMode();
            }
        }

        private void EnableBattleMode()
        {
            _currentPhase = GamePhase.Battle;

            CreateBoard(panelEnemy, true);

            foreach (Control c in panelPlayer.Controls)
                if (c is Button btn) btn.Enabled = false;

            foreach (Control c in panelEnemy.Controls)
                if (c is Button btn) btn.Enabled = true;
        }

        private async void AttackEnemy_Click(object sender, EventArgs e)
        {
            var allMoves = await moveRepository.GetByGameIdAsync(_currentGameId);
            int myMoves = allMoves.Count(m => m.PlayerId == _currentPlayerId);
            int enemyMoves = allMoves.Count(m => m.PlayerId != _currentPlayerId);
            if (myMoves > enemyMoves)
            {
                MessageBox.Show("Ожидайте хода противника!");
                return;
            }

            var btn = (Button)sender;
            var pos = (Point)btn.Tag;

            if (await moveRepository.WasHit(_currentGameId, pos.X, pos.Y))
            {
                MessageBox.Show("Вы уже стреляли в эту клетку!");
                return;
            }
            bool isHit = await _gameService.MakeMove(_currentGameId, _currentPlayerId, pos.X, pos.Y);
            btn.BackColor = isHit ? Color.Red : Color.White;
            btn.Enabled = false;

            if (await _gameService.IsGameOver(_currentGameId, _currentPlayerId))
            {
                _syncTimer.Stop();
                MessageBox.Show("Вы победили!");
                this.Close();
            }
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Text = $"Морской бой (Игрок {_currentPlayerId})";
        }

        private void btnExitFromGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}