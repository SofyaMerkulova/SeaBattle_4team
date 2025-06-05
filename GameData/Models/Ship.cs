namespace GameData.Models
{
    /// <summary>
    /// Сущность корабля
    /// </summary>
    public class Ship
    {
        /// <summary>
        /// Уникальный номер корабля
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Игра корабля по ID
        /// </summary>
        public Guid GameId { get; set; }

        /// <summary>
        /// Игра корабля
        /// </summary>
        public Game? Game { get; set; }

        /// <summary>
        /// Корабль игрока по ID
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        ///  Корабль игрока
        /// </summary>
        public User? Player { get; set; }

        /// <summary>
        /// Тип корабля
        /// </summary>
        public string? ShipType { get; set; }

        /// <summary>
        /// Его расположение
        /// </summary>
        public string Cells { get; set; } = null!;
    }
}
