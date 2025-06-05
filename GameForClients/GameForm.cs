using GameData.Models;
using GameData.Repositories;
using GameData.Repositories.Interfaces;
using GameForClients.Servies.InferfacesForServ;

namespace GameForClients
{
    /// <summary>  
    /// Форма игры и логика для панелей и кнопок
    /// </summary>
    public partial class GameForm : Form
    {

        private readonly IMoveRepository moveRepository;
        private readonly IGameRepository _gameRepo;
        private System.Windows.Forms.Timer gameSyncTimer;

        private bool _isWaitingForOpponent;
        private readonly IGameService _gameService;
        private readonly IShipRepository _shipRepo;
        private readonly Guid _currentPlayerId;
        private Guid _currentGameId;
        private List<Ship> _playerShips = new List<Ship>();

        private HashSet<Point> _enemyHits = new HashSet<Point>();
        private readonly bool _isWinner;
        private bool _isSyncInProgress = false;
        private enum GamePhase { Placement, Battle }
        private GamePhase _currentPhase = GamePhase.Placement;

        public GameForm(IGameService gameService, IGameRepository gameRepo, IMoveRepository moveRepository, Guid playerId, Guid? existingGameId = null)
        {
            this.moveRepository = moveRepository;
            _gameService = gameService;
            _currentPlayerId = playerId;
            _gameRepo = gameRepo;
            _shipRepo = new ShipRepository(new DbForGame());
            InitializeComponent();
            InitializeGameAsync(existingGameId);
            btnCopyId.Click += btnCopyId_Click;
            gameSyncTimer = new System.Windows.Forms.Timer();
            gameSyncTimer.Interval = 1500;
            gameSyncTimer.Tick += SyncGameState;
            gameSyncTimer.Start();
        }
        private void SetupShipPlacement()
        {
            _currentPhase = GamePhase.Placement;
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


        private async void InitializeGameAsync(Guid? existingGameId = null)
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
                    MessageBox.Show($"Ваш ID игры: {_currentGameId}\n Дайте его второму игроку.");
                    SetupShipPlacement();
                    _isWaitingForOpponent = true;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации: {ex.Message}");
                this.Close();
            }

        }

        private async void SyncGameState(object sender, EventArgs e)
        {
            if (_isSyncInProgress) return;
            _isSyncInProgress = true;

            try
            {
                var game = await _gameRepo.GetByIdAsync(_currentGameId);

                if (game.WinnerId != null)
                {
                    gameSyncTimer.Stop();

                    if (game.WinnerId == _currentPlayerId)
                    {
                        var endForm = new ForEndOfGame(true); // победа
                        endForm.ShowDialog();
                    }
                    else
                    {
                        var endForm = new ForEndOfGame(false); // поражение
                        endForm.ShowDialog();
                    }

                    this.Close();
                    return;
                }
                var allMoves = await moveRepository.GetByGameIdAsync(_currentGameId);
                var enemyMoves = allMoves.Where(m => m.PlayerId != _currentPlayerId);

                foreach (var move in enemyMoves)
                {
                    var btn = panelPlayer.Controls.OfType<Button>()
                        .FirstOrDefault(b => ((Point)b.Tag) == new Point(move.X, move.Y));

                    if (btn != null)
                    {
                        bool isHit = _playerShips.Any(s => s.Cells.Split(';')
                            .Contains($"{move.X},{move.Y}"));

                        btn.BackgroundImage = isHit
                            ? Properties.Resources.hit
                            : Properties.Resources.miss;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;

                        if (isHit) _enemyHits.Add(new Point(move.X, move.Y));
                    }
                }

                UpdateShipsCounter();

                var myMoves = allMoves
                    .Where(m => m.PlayerId == _currentPlayerId)
                    .ToList();

                foreach (var move in myMoves)
                {
                    var btn = panelEnemy.Controls.OfType<Button>()
                        .FirstOrDefault(b => ((Point)b.Tag) == new Point(move.X, move.Y));

                    if (btn != null)
                    {
                        btn.BackgroundImage = move.IsHit
                            ? Properties.Resources.hit
                            : Properties.Resources.miss;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                        btn.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка синхронизации: {ex.Message}");
            }
            finally
            {
                _isSyncInProgress = false;
            }
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
        private void UpdateShipsCounter()
        {
            int aliveCells = 0;
            foreach (var ship in _playerShips)
            {
                var cells = ship.Cells.Split(';');
                foreach (var cell in cells)
                {
                    var coords = cell.Split(',');
                    var point = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
                    if (!_enemyHits.Contains(point))
                        aliveCells++;
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
            var game = await _gameRepo.GetByIdAsync(_currentGameId);

            if (game.WinnerId != null && game.WinnerId != _currentPlayerId)
            {
                MessageBox.Show("Вы проиграли. Игра уже завершена!");
                var endForm = new ForEndOfGame(false);
                this.Close();
                return;
            }
            if (game.WinnerId == _currentPlayerId)
            {
                MessageBox.Show("Вы уже победили!");
                return;
            }

            var btn = (Button)sender;
            var pos = (Point)btn.Tag;

            if ((await moveRepository.GetByGameIdAsync(_currentGameId))
                .Any(m => m.PlayerId == _currentPlayerId && m.X == pos.X && m.Y == pos.Y))
            {
                MessageBox.Show("Вы уже стреляли в эту клетку!");
                return;
            }

            bool isHit = await _gameService.MakeMove(_currentGameId, _currentPlayerId, pos.X, pos.Y);
            btn.BackgroundImage = isHit ? Properties.Resources.hit : Properties.Resources.miss;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
            btn.Enabled = false;
            if (await _gameService.IsGameOver(_currentGameId, _currentPlayerId))
            {
                await _gameService.SetWinner(_currentGameId, _currentPlayerId);

                var endForm = new ForEndOfGame(true); // победитель
                endForm.ShowDialog();
                this.Close();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            this.Text = $"Морской бой (ID игры: {_currentGameId})";
        }
        private void btnExitFromGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCopyId_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(_currentGameId.ToString());
                MessageBox.Show("ID игры скопирован в буфер обмена!", "Успешно",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при копировании: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnForEnd_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите сдаться?",
                              "Подтвердите",
                              MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                this.Close();
                var endForm = new ForEndOfGame(false);
                endForm.ShowDialog();

            }
        }
    }
}