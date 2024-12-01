using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kreislauf.Models
{
    public class Klasse
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }

       [ForeignKey("Schule")]
        public int Schule_Id { get; set; }
        public Schule Schule { get; set;}

        public ICollection<Person> Personen { get; set; }

        public static int IndexOf(Klasse klasse)
        {
            throw new NotImplementedException();
        }
    }
}
