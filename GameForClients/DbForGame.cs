using GameForClients.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForClients
{
    public class DbForGame : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Move> Moves { get; set; } = null!;
        public DbSet<Log> Logs { get; set; } = null!;
        public DbSet<Ship> Ships { get; set; } = null!;
    }
}
