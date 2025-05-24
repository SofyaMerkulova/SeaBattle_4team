using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForClients
{
    public class ForCounterShips
    {
        public List<Point> Cells { get; set; } = new List<Point>();

        public bool IsAlive(HashSet<Point> hits)
        {
            return Cells.Any(cell => !hits.Contains(cell));
        }
    }
}
