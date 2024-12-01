using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kreislauf.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        public String Vorname { get; set; }
        public String Nachname { get; set; }

        public int Lebensalter { get; set; }

        public bool? Geschlecht { get; set; }

        [ForeignKey("Klasse")]
        public int? Klasse_Id { get; set; }
        public Klasse Klasse { get; set; }

        public override string ToString()
        {
            return $"Person: {Vorname} {Nachname}: ";
        }
    }
}
