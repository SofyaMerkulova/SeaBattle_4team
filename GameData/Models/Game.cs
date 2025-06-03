namespace GameData.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int? Player1Id { get; set; }
        public User? Player1 { get; set; }

        public int? Player2Id { get; set; }
        public User? Player2 { get; set; }

        public string Status { get; set; } = "Waiting";

        public int? WinnerId { get; set; }
        public User? Winner { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<Move> Moves { get; set; } = new List<Move>();
        public ICollection<Ship> Ships { get; set; } = new List<Ship>();
    }
}
