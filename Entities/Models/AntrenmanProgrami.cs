using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class AntrenmanProgrami
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EgsersizAdi { get; set; }

        [Required]
        public string Hedef { get; set; }

        [Required]
        public int SetTekrarSayilari { get; set; }

        public string? VideoRehberleri { get; set; }

        [Required]
        public DateTime BaslamaTarihi { get; set; }

        [Required]
        public int PrograminSuresi { get; set; }

        
        public int YapanId { get; set; }
        
        public virtual Antrenor YapanAntrenor { get; set; }

   
        public int AliciId { get; set; }
        
        public virtual Kullanici AlanKullanici { get; set; }
    }
}










