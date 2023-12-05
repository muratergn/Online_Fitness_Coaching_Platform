using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Antrenor
    {
        [Key]
        public int Id { get; set; }
        
        public virtual Kullanici Kullanici { get; set; }

        [Required]
        public string Ad { get; set; }

        [Required]
        public string Soyad { get; set; }

        public string UzmanlikAlanlari { get; set; }
        public string Deneyimleri { get; set; }

        [Key]
        [Required]
        [EmailAddress]
        public string Eposta { get; set; }

        public virtual ICollection<AntrenmanProgrami> AntrenmanProgramlari { get; set; }
        public virtual ICollection<BeslenmeProgrami> BeslenmeProgrami { get; set; }
    }
}

