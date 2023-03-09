namespace Center_ElGhalaba.Models
{
    public class Follows
    {
        public int ID { get; set; }
		public int StudentID { get; set; }
		public virtual Student? Student { get; set; }
		public int TeacherID { get; set; }
		public virtual Teacher? Teacher { get; set; }
	}
}
