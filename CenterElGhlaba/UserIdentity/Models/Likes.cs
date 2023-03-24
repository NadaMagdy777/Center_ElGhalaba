using Center_ElGhalaba.Models;

namespace Center_ElGhlaba.Models
{
    public class Likes
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public virtual Student? Student { get; set; }
        public int TeacherID { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}
