using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BeslenmeProgrami
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Hedef { get; set; }

        [Required]
        [MaxLength(255)]
        public string GunlukOgunler { get; set; }

        [Required]
        public int Kalori { get; set; }

        [Required]
        public int YapanId { get; set; }
        
        public virtual Antrenor YapanAntrenor { get; set; }

        [Required]
        public int AliciId { get; set; }
        
        public virtual Kullanici AlanKullanici { get; set; }
    }


}




