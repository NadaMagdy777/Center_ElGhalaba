namespace Center_ElGhalaba.Models
{
    public class Subject
	{
		public int ID { get; set; }
		public string Name { get; set; }
        public virtual List<LevelSubject>? LevelSubjects { get; set; }
		public virtual List<Lesson>? Lessons { get; set; }
	}
}
