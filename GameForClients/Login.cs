using GameData;
using GameData.Models;  
using GameData.Repositories;
using GameForClients.Servies;
using Microsoft.EntityFrameworkCore;
namespace GameForClients
{
    public partial class Login : Form
    {
        private readonly GameService _gameService;
        private readonly DbForGame _dbContext;
        private readonly IGameRepository _gameRepository;
        public Login(DbForGame dbContext, GameService gameService, IGameRepository gameRepository)
        {
            _dbContext = dbContext;
            _gameService = gameService;
            _gameRepository = gameRepository;
            InitializeComponent();
            _dbContext = new DbForGame();
            txtForPassword.PasswordChar = '•';
            checkPassword.CheckedChanged += (s, e) =>
                txtForPassword.PasswordChar = checkPassword.Checked ? '\0' : '•';

            // Обработчики кнопок (подписываемся)
            btnForEnter.Click += async (s, e) => await BtnForEnter_Click(s, e);
            btnForRegistration.Click += async (s, e) => await BtnForRegistration_Click(s, e);
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return ForPasswordGeneration.VerifyPassword(inputPassword, storedHash);
        }
        private async Task BtnForEnter_Click(object sender, EventArgs e)
        {
            string login = txtForLogin.Text.Trim();
            string password = txtForPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var user = await _dbContext.Users
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Username == login);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            MessageBox.Show("Вход выполнен успешно!");
            var menuForm = new Menu(_gameService, _gameRepository, user.Id);
            menuForm.Show();
            this.Hide();
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