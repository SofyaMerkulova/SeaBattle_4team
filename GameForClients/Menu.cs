using GameData.Repositories.Interfaces;
using GameForClients.Servies.InferfacesForServ;
using NLog;
using System.ComponentModel;

namespace GameForClients
{
    /// <summary>
    /// Форма меню
    /// </summary>
    public partial class Menu : Form
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IGameService _gameService;
        private readonly Guid _currentPlayerId;
        private readonly IGameRepository _gameRepo;

        public Menu(IGameService gameService, IGameRepository gameRepo, Guid playerId)
        {
            _gameService = gameService;
            _gameRepo = gameRepo;
            _currentPlayerId = playerId;

            logger.Info("Меню загружено. Игрок:", _currentPlayerId);

            InitializeComponent();
        }
        
        private async void btnForStart_Click(object sender, EventArgs e)
        {
            try
            {
                Guid gameId = await _gameService.CreateGame(_currentPlayerId);
                logger.Info("Создана новая игра. ID: {0}, Игрок:", gameId, _currentPlayerId);

                var gameForm = new GameForm(_gameService, _gameRepo, _gameService.MoveRepo, _currentPlayerId, gameId);
                gameForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при создании новой игры для игрока", _currentPlayerId);
                MessageBox.Show($"Ошибка при создании игры: {ex.Message}");
            }
        }

        private async void btnForJoinGame_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtForID.Text))
                {
                    if (Guid.TryParse(txtForID.Text, out var gameId))
                    {
                        logger.Info("Игрок {0} пытается присоединиться к игре ", _currentPlayerId, gameId);

                        bool joinResult = await _gameService.JoinGame(gameId, _currentPlayerId);

                        if (joinResult)
                        {
                            logger.Info("Игрок {0} успешно присоединился к игре ID: {1}", _currentPlayerId, gameId);

                            var gameForm = new GameForm(_gameService, _gameRepo, _gameService.MoveRepo, _currentPlayerId, gameId);
                            gameForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            logger.Warn("Не удалось подключиться к игре. Игрок: ", gameId, _currentPlayerId);
                            MessageBox.Show("Не удалось осуществить подключение к игре, убедитесь в корректности данных.");
                        }
                    }
                    else
                    {
                        logger.Warn("Некорректный формат ID игры:", txtForID.Text);
                        MessageBox.Show("Некорректный формат ID игры");
                    }

                    return;
                }

                logger.Warn("Попытка присоединиться к игре без ввода ID. Игрок:", _currentPlayerId);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при попытке присоединения к игре игроком", _currentPlayerId);
                MessageBox.Show($"Ошибка при подключении к игре: {ex.Message}");
            }
        }

        private void btnFor_Click(object sender, EventArgs e)
        {
            logger.Info("Игрок вышел из приложения.", _currentPlayerId);
            Application.Exit();
        }
    }
}
