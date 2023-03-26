using Center_ElGhalaba.Models;
using Center_ElGhalaba.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Models;
using Center_ElGhlaba.Models;

namespace UserIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
		public DbSet<Likes> Likes { get; set; }
		public DbSet<LessonLikes> LessonLikes { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("User", "Security");
            builder.Entity<IdentityRole>().ToTable("Role", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Security");

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = RolesConsts.Admin.ToString(),
                    NormalizedName = RolesConsts.Admin.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },

                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = RolesConsts.Teacher.ToString(),
                    NormalizedName = RolesConsts.Teacher.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },

                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = RolesConsts.Student.ToString(),
                    NormalizedName = RolesConsts.Student.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }
            );

            //builder.Entity<IdentityUserRole<string>>().HasData(
            //    new IdentityUserRole<string>
            //    {
            //        UserId = "10e61531-51c6-4109-be14-99c0a4fa4fb2",
            //        RoleId = "5f8e3c6a-aacc-401e-8b81-6e4528b83401"
            //    },
            //    new IdentityUserRole<string>
            //    {
            //        UserId = "10e61531-51c6-4109-be14-99c0a4fa4fb2",
            //        RoleId = "b36c2a5a-e10c-4b82-b86b-20b01a85dc8a"
            //    },
            //    new IdentityUserRole<string>
            //    {
            //        UserId = "10e61531-51c6-4109-be14-99c0a4fa4fb2",
            //        RoleId = "d5e973db-be06-4e57-93f9-6bc4e475f6b9"
            //    }
            //);
        }
    }
}