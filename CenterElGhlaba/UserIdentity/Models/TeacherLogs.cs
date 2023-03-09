namespace Center_ElGhalaba.Models
{
    public class TeacherLogs                            // ====================> XXX
	{
		public int ID { get; set; }

        public int TeacherID { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public int TeacherPaymentMethodID { get; set; }
        public virtual TeacherPaymentMethod? TeacherPaymentMethod { get; set; }
        public decimal Amount { get; set; }
		public DateTime Date { get; set; }
	}
}
