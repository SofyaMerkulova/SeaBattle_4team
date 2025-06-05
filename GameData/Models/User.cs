namespace GameData.Models
{
    /// <summary>
    /// Сущность для пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Его уникальный номер
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Его логин
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Пароль захешированный
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Дата и время
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Игры в качестве первого игрока
        /// </summary>
        public ICollection<Game> GamesAsPlayerFirst { get; set; } = new List<Game>();

        /// <summary>
        /// Игры в качестве второго игрока
        /// </summary>
        public ICollection<Game> GamesAsPlayerSecond { get; set; } = new List<Game>();

        /// <summary>
        /// Игры с результатом победы
        /// </summary>
        public ICollection<Game> GamesWon { get; set; } = new List<Game>();

        /// <summary>
        /// Ходы 
        /// </summary>
        public ICollection<Move> Moves { get; set; } = new List<Move>();

        /// <summary>
        /// Все его корабли
        /// </summary>
        public ICollection<Ship> Ships { get; set; } = new List<Ship>();
    }
}
