using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kreislauf.Models
{
    public class Barcode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        [MaxLength(255)]
        public string Value { get; set; }

        [MaxLength(100)]
        public string Type { get; set; }

        public int? RundenAnzahl { get; set; } = 0;

    }
}
