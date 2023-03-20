using Center_ElGhalaba.Models;

namespace Center_ElGhlaba.ViewModels
{
    public class AdminUsersVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
