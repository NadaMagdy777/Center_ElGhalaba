using Center_ElGhalaba.Models;

namespace Center_ElGhlaba.Models
{
    public class LessonLikes
    {
        public int ID { get; set; }
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson? Lesson { get; set; }
    }
}
