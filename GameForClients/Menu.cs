using GameData.Repositories;
using GameForClients.Properties;
using GameForClients.Servies;
using System;
using System.Windows.Forms;

namespace GameForClients
{
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

        private async void BtnCreateGame_Click(object sender, EventArgs e)
        {
            var gameForm = new GameForm(_gameService, _gameRepo, _currentPlayerId);
            gameForm.Show();
            this.Hide();
        }

        private async void BtnJoinGame_Click(object sender, EventArgs e)
        {
            var availableGames = await _gameService.FindOpenGames(_currentPlayerId);
            if (availableGames.Count == 0)
            {
                MessageBox.Show("Нет доступных игр");
                return;
            }

            var selectedGame = availableGames.First();
            var gameForm = new GameForm(_gameService, _gameRepo, _currentPlayerId, selectedGame.Id);
            gameForm.Show();
            this.Hide();
        }
    }
}