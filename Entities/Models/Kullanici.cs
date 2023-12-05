using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Kullanici
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ad { get; set; }

        [Required]
        public string Soyad { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DogumTarihi { get; set; }

        [Required]
        public string Cinsiyet { get; set; }

        [Key]
        [Required]
        [EmailAddress]
        public string Eposta { get; set; }

        public string? TelefonNumarasi { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        public string? ProfilFotosu { get; set; }

        public string Etkinlik { get; set; }

        public string Rolu { get; set; }

        public virtual ICollection<AntrenmanProgrami> AntrenmanProgramlari { get; set; }
        public virtual ICollection<BeslenmeProgrami> BeslenmeProgramlari { get; set; }
        public virtual ICollection<Antrenor> Antrenorlar { get; set; }
        public virtual ICollection<Gelisme> Gelismeler { get; set; }
    }


}









