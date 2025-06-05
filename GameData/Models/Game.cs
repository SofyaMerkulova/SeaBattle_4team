namespace GameData.Models
{
    /// <summary>
    /// Статус игры
    /// </summary>
    public enum GameStatus
    {
        /// <summary>
        /// Ожидание
        /// </summary>
        Waiting,

        /// <summary>
        /// Расставление кораблей
        /// </summary>
        PlacingShips,

        /// <summary>
        /// Процесс игры
        /// </summary>
        InProgress,

        /// <summary>
        /// Завершение
        /// </summary>
        Finished
    }
    /// <summary>
    /// Сущность игры
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Уникальный номер игры
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Первый игрок по его номеру
        /// </summary>
        public Guid? PlayerFirstId { get; set; }

        /// <summary>
        /// Первый игрок
        /// </summary>
        public User? PlayerFirst { get; set; }

        /// <summary>
        /// Второй игрок по его номеру
        /// </summary>
        public Guid? PlayerSecondId { get; set; }

        /// <summary>
        /// Второй игрок
        /// </summary>
        public User? PlayerSecond { get; set; }

        /// <summary>
        /// Статус игры
        /// </summary>
        public GameStatus Status { get; set; } = GameStatus.Waiting;

        /// <summary>
        /// Кто победил по номеру
        /// </summary>
        public Guid? WinnerId { get; set; }

        /// <summary>
        /// Кто победил
        /// </summary>
        public User? Winner { get; set; }

        /// <summary>
        /// Когда была создана игра
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Все ходы в игре
        /// </summary>
        public ICollection<Move> Moves { get; set; } = new List<Move>();

        /// <summary>
        /// Все корабли в игре
        /// </summary>
        public ICollection<Ship> Ships { get; set; } = new List<Ship>();
    }
}
