using System.ComponentModel.DataAnnotations;

namespace Online_Fitness_Coaching_Platform.Models
{
    public class AntrenorEditModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string? ImageUrl { get; set; }

        public string? Active { get; set; }

        public string? Status { get; set; }

        public string? UzmanlikAlanlari { get; set; }
        public string? Deneyimleri { get; set; }
    }
}
