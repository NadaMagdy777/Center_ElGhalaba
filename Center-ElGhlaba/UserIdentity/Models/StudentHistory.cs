namespace Center_ElGhalaba.Models
{
    public class StudentHistory
    {
        public int ID { get; set; }
        public int StudentID { get; set;}
        public virtual Student? Student { get; set; }
        public int LessonID { get; set; }
        public virtual Lesson? Lesson{ get; set; }
		public DateTime Date { get; set; }
	}
}
