using GameForClients.Properties;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForClients.Models
{
    public class Ship
    {
        public int Id { get; set; }

        public int GameId { get; set; }
        public Game? Game { get; set; }

        public int PlayerId { get; set; }
        public User? Player { get; set; }

        public string? ShipType { get; set; }
        public string Cells { get; set; } = null!;
    }
}
