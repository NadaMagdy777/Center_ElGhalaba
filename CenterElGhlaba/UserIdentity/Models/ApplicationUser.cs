using Center_ElGhalaba.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UserIdentity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength (100)]
        public string FirstName { get; set; }
        [Required, MaxLength (100)]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        public byte[]? Image { get; set; }
		public DateTime Birthdate { get; set; }
		public DateTime JoinDate { get; set; }
		public bool IsAvailable { get; set; }
		public bool IsDeleted { get; set; }
        [JsonIgnore]
		public virtual Student? Student { get; set; }
        [JsonIgnore]
		public virtual Teacher? Teacher { get; set; }
	}
}
