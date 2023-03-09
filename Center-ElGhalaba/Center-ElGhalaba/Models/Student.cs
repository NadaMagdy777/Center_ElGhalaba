using System.ComponentModel.DataAnnotations.Schema;

namespace Center_ElGhalaba.Models
{
    public class Student
    {
        public int ID { get; set; }
        [ForeignKey ("AppUser")]
        public string AppUserID { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual List<StudentOrder>? Orders { get; set; }
		public virtual List<LessonComment>? Comments { get; set; }
		public virtual List<StudentHistory>? History { get; set; }
		public virtual List<Follows>? Follows { get; set; }
	}
}
