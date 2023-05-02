using Center_ElGhalaba.Models;
using System.Text.Json.Serialization;

namespace Center_ElGhlaba.Models
{
    public class LessonLikes
    {
        public int ID { get; set; }
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public int LessonId { get; set; }
        [JsonIgnore]
        public virtual Lesson? Lesson { get; set; }
    }
}
