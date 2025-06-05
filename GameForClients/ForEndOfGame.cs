using GameData.Repositories.Interfaces;
using GameForClients.Servies.InferfacesForServ;

namespace GameForClients
{
    public partial class ForEndOfGame : Form
    {
       
        private readonly Guid playerId;
        private readonly IGameService _gameService;
        private readonly IUserRepository _userRepo;
        private readonly DbForGame _dbContext;
        private readonly IGameRepository _gameRepo;
        private readonly bool _isWinner;
       

        public ForEndOfGame(bool isWinner)
        {
            InitializeComponent();
            _isWinner = isWinner;
            
            lblGameName.Text = _isWinner
                ? " Вы победили!"
                : "Вы проиграли";
        }

        private void btnForEndGame_Click(object sender, EventArgs e)
        {
            var menuForm = new Menu(_gameService,_gameRepo,playerId); 
            menuForm.Show();
            this.Close();
        }

        private void btnForAgainStartGame_Click(object sender, EventArgs e)
        {
            var loginForm = new Login(_dbContext, _gameService, _gameRepo, _userRepo);
            loginForm.Show();
            this.Close();
        }
    }
}