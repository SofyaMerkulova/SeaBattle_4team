using GameData.Models;
using GameData.Repositories;
using GameData.Repositories.Interfaces;
using GameForClients.Servies.InferfacesForServ;
using NLog;

namespace GameForClients
{
    /// <summary>  
    /// Форма игры и логика для панелей и кнопок
    /// </summary>
    public partial class GameForm : Form
    {

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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
                logger.Info("Инициализация игры. Игрок: {0}, GameId: {1}", _currentPlayerId, existingGameId?.ToString() ?? "новая");

                if (existingGameId.HasValue)
                {
                    _currentGameId = existingGameId.Value;
                    if (!await _gameService.JoinGame(_currentGameId, _currentPlayerId))
                    {
                        MessageBox.Show("");
                        this.Close();
                        return;
                    }

                    _playerShips = (await _shipRepo.GetByGameIdAsync(_currentGameId))
                        .Where(s => s.PlayerId == _currentPlayerId).ToList();

                    CreateGameBoards();

                    if (_playerShips.Count == 0)
                    {
                        MessageBox.Show("Расставьте свои корабли");
                        SetupShipPlacement();
                    }
                    else
                    {
                        _currentPhase = GamePhase.Battle;
                        MessageBox.Show("Все корабли размещены!");
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
                logger.Error(ex, "Ошибка при инициализации игры.");
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
                logger.Debug("Синхронизация состояния игры. GameId: {0}", _currentGameId);

                var game = await _gameRepo.GetByIdAsync(_currentGameId);

                if (game.WinnerId != null)
                {
                    logger.Info("Игра завершена. Победитель: {0}", game.WinnerId);
                    gameSyncTimer.Stop();

                    var endForm = new ForEndOfGame(game.WinnerId == _currentPlayerId);
                    endForm.ShowDialog();
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

                        btn.BackgroundImage = isHit ? Properties.Resources.hit : Properties.Resources.miss;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;

                        if (isHit) _enemyHits.Add(new Point(move.X, move.Y));
                    }
                }

                UpdateShipsCounter();

                var myMoves = allMoves.Where(m => m.PlayerId == _currentPlayerId).ToList();

                foreach (var move in myMoves)
                {
                    var btn = panelEnemy.Controls.OfType<Button>()
                        .FirstOrDefault(b => ((Point)b.Tag) == new Point(move.X, move.Y));

                    if (btn != null)
                    {
                        btn.BackgroundImage = move.IsHit ? Properties.Resources.hit : Properties.Resources.miss;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                        btn.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при синхронизации состояния игры.");
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
                panel.Controls.Add(new Label
                {
                    Text = ((char)('А' + col)).ToString(),
                    Size = new Size(cellSize, cellSize),
                    Location = new Point((col + 1) * cellSize, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White
                });
            }

            for (int row = 0; row < 10; row++)
            {
                panel.Controls.Add(new Label
                {
                    Text = (row + 1).ToString(),
                    Size = new Size(cellSize, cellSize),
                    Location = new Point(0, (row + 1) * cellSize),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White
                });

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
                        btn.Click += PlaceShip_Click;
                    else if (isEnemy && _currentPhase == GamePhase.Battle)
                        btn.Click += AttackEnemy_Click;

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
                logger.Warn("Попытка разместить корабль в занятой клетке", pos.X, pos.Y);
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

            logger.Info("Корабль размещен ", _currentPlayerId, pos.X, pos.Y);

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
            var btn = (Button)sender;
            var pos = (Point)btn.Tag;

            logger.Info("Игрок атакует позицию)", _currentPlayerId, pos.X, pos.Y);

            var game = await _gameRepo.GetByIdAsync(_currentGameId);

            if (game.WinnerId != null && game.WinnerId != _currentPlayerId)
            {
                logger.Warn("Игрок попытался стрелять в завершённой игре");
                MessageBox.Show("Вы проиграли. Игра уже завершена!");
                this.Close();
                return;
            }

            if (game.WinnerId == _currentPlayerId)
            {
                MessageBox.Show("Вы уже победили!");
                return;
            }

            if ((await moveRepository.GetByGameIdAsync(_currentGameId))
                .Any(m => m.PlayerId == _currentPlayerId && m.X == pos.X && m.Y == pos.Y))
            {
                logger.Warn("Повторный выстрел", pos.X, pos.Y, _currentPlayerId);
                MessageBox.Show("Вы уже стреляли в эту клетку!");
                return;
            }

            bool isHit = await _gameService.MakeMove(_currentGameId, _currentPlayerId, pos.X, pos.Y);
            btn.BackgroundImage = isHit ? Properties.Resources.hit : Properties.Resources.miss;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
            btn.Enabled = false;

            if (await _gameService.IsGameOver(_currentGameId, _currentPlayerId))
            {
                logger.Info("Игрок {0} победил в игре {1}", _currentPlayerId, _currentGameId);
                await _gameService.SetWinner(_currentGameId, _currentPlayerId);
                var endForm = new ForEndOfGame(true);
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
                logger.Info("ID игры скопирован: {0}", _currentGameId);
                MessageBox.Show("ID игры скопирован в буфер обмена!", "Успешно",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при копировании ID игры.");
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
                logger.Info("Игрок сдался", _currentPlayerId);
                this.Close();
                var endForm = new ForEndOfGame(false);
                endForm.ShowDialog();
            }
        }
    }
}