using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForClients.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int? Player1Id { get; set; }
        public User? Player1 { get; set; }

        public int? Player2Id { get; set; }
        public User? Player2 { get; set; }

        public int? WinnerId { get; set; }
        public User? Winner { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public ICollection<Move> Moves { get; set; } = new List<Move>();
        public ICollection<Ship> Ships { get; set; } = new List<Ship>();
    }
}
