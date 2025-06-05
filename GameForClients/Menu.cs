using GameData.Repositories.Interfaces;
using GameForClients.Servies;
using GameForClients.Servies.InferfacesForServ;

namespace GameForClients
{
    /// <summary>  
    /// Форма меню выбор начала игры или присоединения уже к существующей
    /// </summary>
    public partial class Menu : Form
    {
        private readonly IGameService _gameService;
        private readonly Guid _currentPlayerId;
        private readonly IGameRepository _gameRepo;

        public Menu(IGameService gameService, IGameRepository gameRepo, Guid playerId)
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
                Guid gameId = await _gameService.CreateGame(_currentPlayerId);

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
                if (!string.IsNullOrEmpty(txtForID.Text))
                {
                    if (Guid.TryParse(txtForID.Text, out var gameId))
                    {
                        bool joinResult = await _gameService.JoinGame(gameId, _currentPlayerId);

                        if (joinResult)
                        {
                            var gameForm = new GameForm(_gameService, _gameRepo, _gameService.MoveRepo, _currentPlayerId, gameId);
                            gameForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось осуществить подключение к игре, убедитесь в корректности данных.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Некорректный формат ID игры");
                    }
                    return;
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