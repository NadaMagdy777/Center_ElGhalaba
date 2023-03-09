using System.ComponentModel.DataAnnotations.Schema;

namespace Center_ElGhalaba.Models
{
    public class Teacher
    {
        public int ID { get; set; }
        public decimal Balance { get; set; }
        public int Likes { get; set; }
		public virtual List<Lesson>? Lessons { get; set; }
		public virtual List<TeacherPaymentMethod>? PaymentMethods { get; set; }
		public virtual List<Follows>? Follows { get; set; }
		public virtual List<TeacherLogs>? Logs { get; set; }
		[ForeignKey("AppUser")]
		public string AppUserID { get; set; }
		public virtual AppUser? AppUser { get; set; }
	}
}
