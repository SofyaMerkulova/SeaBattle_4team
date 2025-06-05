namespace GameData.Models
{
    /// <summary>
    /// Сущность хода
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Уникальный номер хода
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ход к какой игре по ID
        /// </summary>
        public Guid GameId { get; set; }

        /// <summary>
        /// Ход к игре
        /// </summary>
        public Game? Game { get; set; }

        /// <summary>
        /// Ход игрока по ID
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Ход игрока
        /// </summary>
        public User? Player { get; set; }

        /// <summary>
        /// Его расположение
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Его расположение
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Проверка на попадение
        /// </summary>
        public bool IsHit { get; set; }

        /// <summary>
        /// Время хода
        /// </summary>
        public DateTime MoveTime { get; set; } = DateTime.UtcNow;
    }
}
