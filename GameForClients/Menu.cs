using GameData.Repositories;
using GameForClients.Servies;

namespace GameForClients
{
    /// <summary>  
    /// Форма меню выбор начала игры или присоединения уже к существующей
    /// </summary>
    public partial class Menu : Form
    {
        private readonly GameService _gameService;
        private readonly int _currentPlayerId;
        private readonly IGameRepository _gameRepo;

        public Menu(GameService gameService, IGameRepository gameRepo, int playerId)
        {
            _gameService = gameService;
            _gameRepo = gameRepo;
            _currentPlayerId = playerId;
            InitializeComponent();
        }

        private async void btnForStart_Click(object sender, EventArgs e)
        {
            try
            {
                int gameId = await _gameService.CreateGame(_currentPlayerId);

                var gameForm = new GameForm(_gameService, _gameRepo, _gameService.MoveRepo, _currentPlayerId, gameId);
                gameForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании игры: {ex.Message}");
            }
        }

        private async void btnForJoinGame_Click_1(object sender, EventArgs e)
        {
            try
            {
                var availableGames = await _gameService.FindOpenGames(_currentPlayerId);
                if (availableGames.Count == 0)
                {
                    MessageBox.Show("Нет доступных игр для подключения");
                    return;
                }

                var selectedGame = availableGames[0];
                bool joinResult = await _gameService.JoinGame(selectedGame.Id, _currentPlayerId);

                if (joinResult)
                {
                    var gameForm = new GameForm(_gameService, _gameRepo, _gameService.MoveRepo, _currentPlayerId, selectedGame.Id);
                    gameForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Не удалось присоединиться к игре");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к игре: {ex.Message}");
            }
        }

        private void btnFor_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}