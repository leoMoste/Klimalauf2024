using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kreislauf.Models
{
    public class Schule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Stadt { get; set; }

        public ICollection<Klasse> Klassen { get; set; }
    }
}
