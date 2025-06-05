using GameData;
using GameData.Models;
using GameData.Repositories.Interfaces;
using GameForClients.Servies.InferfacesForServ;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.ComponentModel;
using System.Globalization;

namespace GameForClients
{
    /// <summary>  
    /// Форма регистрации и входа
    /// </summary>
    public partial class Login : Form
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

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
        private void ApplyLocalization()
        {
            var resources = new ComponentResourceManager(this.GetType());

            foreach (Control control in this.Controls)
            {
                resources.ApplyResources(control, control.Name);
                ApplyResourcesToChildren(control, resources);
            }

            resources.ApplyResources(this, "$this");
        }

        private void ApplyResourcesToChildren(Control parent, ComponentResourceManager resources)
        {
            foreach (Control child in parent.Controls)
            {
                resources.ApplyResources(child, child.Name);
                if (child.HasChildren)
                    ApplyResourcesToChildren(child, resources);
            }
        }
        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return ForPasswordGeneration.VerifyPassword(inputPassword, storedHash);
        }

        private async Task BtnForEnter_Click(object sender, EventArgs e)
        {
            var login = txtForLogin.Text;
            var password = txtForPassword.Text;

            var user = await AuthenticateUser(login, password);
            if (user == null)
            {
                _logger.Warn($"Неудачная попытка входа с логином: '{login}'");
                return;
            }

            _logger.Info($"Пользователь '{user.Username}' успешно вошёл в систему.");

            var activeGame = await _gameRepository.GetActiveGameAsync();

            if (activeGame == null)
            {
                var gameId = await _gameService.CreateGame(user.Id);
                _logger.Info($"Создана новая игра (ID={gameId}) для пользователя '{user.Username}'.");

                var menuForm = new Menu(_gameService, _gameRepository, user.Id);
                menuForm.Show();
                this.Hide();
            }
            else
            {
                if (await _gameService.JoinGame(activeGame.Id, user.Id))
                {
                    _logger.Info($"Пользователь '{user.Username}' присоединился к активной игре (ID={activeGame.Id}).");

                    var menuForm = new Menu(_gameService, _gameRepository, user.Id);
                    menuForm.Show();
                    this.Hide();
                }
                else
                {
                    _logger.Warn($"Пользователь '{user.Username}' не смог присоединиться к игре (ID={activeGame.Id}).");
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
                MessageBox.Show("Неверный логин или пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                _logger.Warn("Попытка регистрации с коротким логином.");
                return;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать минимум 6 символов", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _logger.Warn($"Попытка регистрации с коротким паролем (логин: '{login}').");
                return;
            }

            if (_dbContext.Users.Any(u => u.Username == login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _logger.Warn($"Попытка регистрации с уже существующим логином: '{login}'");
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
            _logger.Info($"Новый пользователь зарегистрирован: '{login}'");
        }

        private void btnForEn_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            ApplyLocalization();
        }

        private void btnForRu_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru");
            ApplyLocalization();
        }
    }
}
