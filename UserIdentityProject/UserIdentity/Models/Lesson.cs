namespace Center_ElGhalaba.Models
{
    public class Lesson
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual Level? Level { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual List<LessonResource>? Resources { get; set; }
        public virtual List<LessonQuiz>? Quizzes { get; set; }
		public virtual List<StudentOrder>? Orders { get; set; }
		public virtual List<LessonComment>? Comments { get; set; }
		public virtual List<StudentHistory>? History { get; set; }
	}
}
