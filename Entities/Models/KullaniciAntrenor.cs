using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class KullaniciAntrenor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DanisanId { get; set; }

        [Required]
        public int AntrenorId { get; set; }
    }
}
