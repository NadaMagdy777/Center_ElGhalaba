using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Center_ElGhalaba.Models
{
	public class Stage
	{
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual List<Level>? Levels { get; set; }
    }

    public class Level
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int StageID { get; set; }
        public virtual Stage? Stage { get; set; }
		public virtual List<Lesson>? Lessons { get; set; }
	}

	public class Subject
	{
		public int ID { get; set; }
		public string Name { get; set; }
        public virtual List<LevelSubject>? LevelSubjects { get; set; }
		public virtual List<Lesson>? Lessons { get; set; }
	}

	public class LevelSubject
    {
        public int ID { get; set; }
        public int LevelID { get; set; }
        public virtual Level? Level { get; set; }
        public int StageID { get; set; }
        public virtual Stage? Stage { get; set; }
    }

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

	public class LessonResource
    {
        public int ID { get; set; }
        public int LessonID { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
		public bool IsDeleted { get; set; }
	}

	public class LessonQuiz
    {
        public int ID { get; set; }
        public int LessonID { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
		public bool IsDeleted { get; set; }
	}

	public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] ProfilePicture { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }

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

    public class StudentOrder
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public virtual Student? Student { get; set; }
        public int LessonID { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public string PaymentName { get; set; }
        public string PaymentVlaue { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
        public string PaymentValue { get; set; }
        public bool IsWatching { get; set; }
    }

    public class LessonComment
    {
        public int ID { get; set; }
        public int LessonID { get; set;}
        public virtual Lesson? Lesson { get; set; }
        public int StudentID { get; set; }
        public virtual Student? Student { get; set; }
        public string  Body { get; set; }
        public DateTime Date { get; set; }
    }

    public class StudentHistory
    {
        public int ID { get; set; }
        public int StudentID { get; set;}
        public virtual Student? Student { get; set; }
        public int LessonID { get; set; }
        public virtual Lesson? Lesson{ get; set; }
		public DateTime Date { get; set; }
	}

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

    public class TeacherPaymentMethod                   // ====================> XXX
	{
        public int ID { get; set; }
        public int TeacherID { get; set; }
        public virtual List<TeacherLogs>? Logs { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public string PaymentName { get; set; }
		public string PaymentVlaue { get; set; }
	}

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

    public class Follows
    {
        public int ID { get; set; }
		public int StudentID { get; set; }
		public virtual Student? Student { get; set; }
		public int TeacherID { get; set; }
		public virtual Teacher? Teacher { get; set; }
	}
}
