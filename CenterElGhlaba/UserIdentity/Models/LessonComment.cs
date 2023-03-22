namespace Center_ElGhalaba.Models
{
    public class LessonComment
    {
        public int ID { get; set; }
        public int LessonID { get; set;}
        public virtual Lesson? Lesson { get; set; }
        public int StudentID { get; set; }
        public virtual Student? Student { get; set; }
        public string  Body { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
    }
}
