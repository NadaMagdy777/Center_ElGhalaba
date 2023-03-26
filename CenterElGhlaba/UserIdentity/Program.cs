using Center_ElGhalaba.Helpers;
using Center_ElGhlaba.Hubs;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.Repositories;
using Center_ElGhlaba.Services;
using Center_ElGhlaba.Unit_OfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UserIdentity.Data;
using UserIdentity.Models;

namespace UserIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddSignalR();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireDigit = false;
                    opt.SignIn.RequireConfirmedAccount = true;
                }
            )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<ApplicationUser>>();

            builder.Services.AddControllersWithViews();

            builder.Services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 104857600;
            });

            builder.Services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 104857600;
            });

            //builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddTransient<StudentServices, StudentServices>();
            builder.Services.AddTransient<AdminServices, AdminServices>();


            //builder.Services.AddHealthChecks();
            builder.Services.AddRazorPages();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.MapHub<CommentHub>("/lessonComment");

            app.MapHub<TeacherHub>("/TeacherHub");
            
            app.MapHub<LessonHub>("/NewLesson");

            app.MapHub<UserHub>("/UserHub");

            app.MapHub<LessonLikesHub>("/LessonLikesHub");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}