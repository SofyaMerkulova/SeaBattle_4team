using GameData;
using GameData.Models;
using GameData.Repositories.Interfaces;
using GameForClients.Servies.InferfacesForServ;
using Microsoft.EntityFrameworkCore;
namespace GameForClients
{
    /// <summary>  
    /// Форма регистрации и входа, логика для кнопок и полей
    /// </summary>
    public partial class Login : Form
    {
        private readonly IGameService _gameService;
        private readonly IUserRepository _userRepository;
        private readonly DbForGame _dbContext;
        private readonly IGameRepository _gameRepository;
        private static int _currentGameId = 0;
        private static int _playersLoggedIn = 0;
        private static List<int> _playerIds = new List<int>();
        public Login(DbForGame dbContext, IGameService gameService,
            IGameRepository gameRepository, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _gameService = gameService;
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            InitializeComponent();
            txtForPassword.PasswordChar = '•';
            checkPassword.CheckedChanged += (s, e) =>
                txtForPassword.PasswordChar = checkPassword.Checked ? '\0' : '•';

            btnForEnter.Click += async (s, e) => await BtnForEnter_Click(s, e);
            btnForRegistration.Click += async (s, e) => await BtnForRegistration_Click(s, e);
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return ForPasswordGeneration.VerifyPassword(inputPassword, storedHash);
        }
        private async Task BtnForEnter_Click(object sender, EventArgs e)
        {
            var user = await AuthenticateUser(txtForLogin.Text, txtForPassword.Text);
            if (user == null) return;

            var activeGame = await _gameRepository.GetActiveGameAsync();

            if (activeGame == null)
            {

                var gameId = await _gameService.CreateGame(user.Id);
                var menuForm = new Menu(_gameService, _gameRepository, user.Id);
                menuForm.Show();
                this.Hide();
            }
            else
            {
                if (await _gameService.JoinGame(activeGame.Id, user.Id))
                {
                    var menuForm = new Menu(_gameService, _gameRepository, user.Id);
                    menuForm.Show();
                    this.Hide();
                }
            }
        }
        private async Task<User> AuthenticateUser(string login, string password)
        {
            using var context = new DbForGame();
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == login);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                MessageBox.Show("Неверный логин или пароль");
                return null;
            }

            return user;
        }
        private async Task BtnForRegistration_Click(object sender, EventArgs e)
        {
            string login = txtForLogin.Text.Trim();
            string password = txtForPassword.Text;


            if (string.IsNullOrEmpty(login) || login.Length < 4)
            {
                MessageBox.Show("Логин должен содержать минимум 4 символа", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать минимум 6 символов", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_dbContext.Users.Any(u => u.Username == login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = new User
            {
                Username = login,
                PasswordHash = ForPasswordGeneration.HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            MessageBox.Show("Регистрация прошла успешно!", "Успех",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}