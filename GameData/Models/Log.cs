namespace GameData.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string? Action { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
