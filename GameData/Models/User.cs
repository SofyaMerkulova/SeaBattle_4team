namespace GameData.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public ICollection<Game> GamesAsPlayer1 { get; set; } = new List<Game>();
        public ICollection<Game> GamesAsPlayer2 { get; set; } = new List<Game>();
        public ICollection<Game> GamesWon { get; set; } = new List<Game>();
        public ICollection<Move> Moves { get; set; } = new List<Move>();
        public ICollection<Log> Logs { get; set; } = new List<Log>();
        public ICollection<Ship> Ships { get; set; } = new List<Ship>();
    }
}