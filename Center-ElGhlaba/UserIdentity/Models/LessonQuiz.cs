namespace Center_ElGhalaba.Models
{
    public class LessonQuiz
    {
        public int ID { get; set; }
        public int LessonID { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
		public bool IsDeleted { get; set; }
	}
}
