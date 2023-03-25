using Center_ElGhlaba.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Center_ElGhalaba.Models
{
    public class Lesson
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public byte[]? CoverPicture { get; set; }
        public DateTime PublishDate { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public virtual List<LessonLikes>? Likes { get; set; }
        public virtual List<LessonViews>? Views { get; set; }   
        public bool IsDeleted { get; set; }
       
        [ForeignKey("Subject")]
        public int subjectID { get; set; }

        [ForeignKey("Level")]
        public int levelID { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherID { get; set; }

        [JsonIgnore]
        public virtual Subject? Subject { get; set; }
        [JsonIgnore]
        public virtual Level? Level { get; set; }
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }
        [JsonIgnore]
        public virtual List<LessonResource>? Resources { get; set; }
        [JsonIgnore]
        public virtual List<LessonQuiz>? Quizzes { get; set; }
        [JsonIgnore]
        public virtual List<StudentOrder>? Orders { get; set; }
        [JsonIgnore]
        public virtual List<LessonComment>? Comments { get; set; }
        [JsonIgnore]
        public virtual List<StudentHistory>? History { get; set; }
	}
}
