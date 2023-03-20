namespace Center_ElGhlaba.ViewModels
{
    public class AdminCommentVM
    {
        public int ID { get; set; }
        public int LessonID { get; set; }
        public string Lesson { get; set; }
        public string Student { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }
}
