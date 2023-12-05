using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Mesajlasma
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GonderenId { get; set; }

        [Required]
        public int AliciId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Mesaj { get; set; }
    }
}
