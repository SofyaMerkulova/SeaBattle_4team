namespace GameData.Models
{
    public class Ship
    {
        public Guid Id { get; set; }

        public Guid GameId { get; set; }
        public Game? Game { get; set; }

        public Guid PlayerId { get; set; }
        public User? Player { get; set; }

        public string? ShipType { get; set; }
        public string Cells { get; set; } = null!;
    }
}
