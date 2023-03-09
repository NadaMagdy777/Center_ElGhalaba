namespace Center_ElGhalaba.Models
{
    public class TeacherPaymentMethod                   // ====================> XXX
	{
        public int ID { get; set; }
        public int TeacherID { get; set; }
        public virtual List<TeacherLogs>? Logs { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public string PaymentName { get; set; }
		public string PaymentVlaue { get; set; }
	}
}
