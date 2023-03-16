namespace Center_ElGhalaba.Models
{
    public class StudentOrder
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public virtual Student? Student { get; set; }
        public int LessonID { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public string PaymentName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
        public string PaymentValue { get; set; }
        public bool IsWatching { get; set; }
    }
}
