using System.ComponentModel.DataAnnotations;

namespace Online_Fitness_Coaching_Platform.Models
{
    public class KullaniciEditModel
    {
        [Required]
        public string Ad { get; set; }

        [Required]
        public string Soyad { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DogumTarihi { get; set; }

        [Required]
        public string Cinsiyet { get; set; }

        [EmailAddress]
        public string Eposta { get; set; }

        public string? TelefonNumarasi { get; set; }

        public string? Aktiflik { get; set; }
    }
}
