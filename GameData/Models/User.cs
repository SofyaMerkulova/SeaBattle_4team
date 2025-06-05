namespace GameData.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public ICollection<Game> GamesAsPlayerFirst { get; set; } = new List<Game>();
        public ICollection<Game> GamesAsPlayerSecond { get; set; } = new List<Game>();
        public ICollection<Game> GamesWon { get; set; } = new List<Game>();
        public ICollection<Move> Moves { get; set; } = new List<Move>();
        public ICollection<Ship> Ships { get; set; } = new List<Ship>();
    }
}