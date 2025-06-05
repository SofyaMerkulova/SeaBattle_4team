namespace GameData.Models
{
    public enum GameStatus
    {
        Waiting,
        PlacingShips,
        InProgress,
        Finished
    }

    public class Game
    {
        public Guid Id { get; set; }
        public Guid? PlayerFirstId { get; set; }
        public User? PlayerFirst { get; set; }

        public Guid? PlayerSecondId { get; set; }
        public User? PlayerSecond { get; set; }

        public GameStatus Status { get; set; } = GameStatus.Waiting;

        public Guid? WinnerId { get; set; }
        public User? Winner { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<Move> Moves { get; set; } = new List<Move>();
        public ICollection<Ship> Ships { get; set; } = new List<Ship>();
    }
}
