using Center_ElGhalaba.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Center_ElGhalaba.Data
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
	{
		public DbSet<Level> Levels { get; set; }
		public DbSet<Stage> Stages { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<LevelSubject> LevelsSubject { get; set; }
		public DbSet<Lesson> Lessons { get; set; }
		public DbSet<LessonResource> LessonResources { get; set; }
		public DbSet<LessonQuiz> LessonQuizes { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<StudentOrder> StudentOrders { get; set; }
		public DbSet<StudentHistory> StudentHistory { get; set; }
		public DbSet<LessonComment> LessonComments { get; set; }
		public DbSet<TeacherPaymentMethod> TeacherPaymentMethod { get; set; }
		public DbSet<TeacherLogs> TeacherLogs { get; set; }
		public DbSet<Follows> Follows { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	}
}