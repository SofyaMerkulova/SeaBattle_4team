namespace GameData.Models
{
    public class Move
    {
        public Guid Id { get; set; }

        public Guid GameId { get; set; }
        public Game? Game { get; set; }

        public Guid PlayerId { get; set; }
        public User? Player { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public bool IsHit { get; set; }
        public DateTime MoveTime { get; set; } = DateTime.UtcNow;
    }
}
