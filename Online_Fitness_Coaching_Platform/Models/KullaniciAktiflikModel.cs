using System.ComponentModel.DataAnnotations;

namespace Online_Fitness_Coaching_Platform.Models
{
    public class KullaniciAktiflikModel
    {
        [Required]
        public string Isim { get; set; }
        [Required]
        public string Soyisim { get; set; }
        [Required]
        public string Eposta { get; set; }
        [Required]
        public string Rol { get; set; }
        [Required]
        public string Aktiflik { get; set; }
        [Required]
        public string AktiflikDurum { get; set; }


    }
}
