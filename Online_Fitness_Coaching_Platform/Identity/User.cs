using Microsoft.AspNetCore.Identity;

namespace Online_Fitness_Coaching_Platform.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
