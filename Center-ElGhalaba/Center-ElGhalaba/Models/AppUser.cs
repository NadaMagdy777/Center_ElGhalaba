using Microsoft.AspNetCore.Identity;

namespace Center_ElGhalaba.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] ProfilePicture { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}
