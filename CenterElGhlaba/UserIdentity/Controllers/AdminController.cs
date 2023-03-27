using Center_ElGhalaba.Constants;
using Center_ElGhlaba.Services;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using UserIdentity.Data;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UserIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Center_ElGhalaba.Models;

namespace Center_ElGhlaba.Controllers
{
    [Authorize (Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AdminServices services;
        private readonly ApplicationDbContext context;

        public AdminController(AdminServices services, ApplicationDbContext context)
        {
            this.services = services;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Students()
        {
            return View();
        }

        public IActionResult DeletedStudents()
        {
            return View();
        }

        public IActionResult Teachers()
        {
            return View();
        }

        public IActionResult DeletedTeachers()
        {
            return View();
        }
        
        public IActionResult Lessons()
        {
            return View();
        }

        public IActionResult DeletedLessons()
        {
            return View();
        }

        public IActionResult Comments()
        {
            return View();
        }
        
        public IActionResult AddNew()
        {
            List<Stage> stages = context.Stages.ToList();
            List<Level> levels = context.Levels.ToList();
            AdminAddNewVM vm = new()
            {
                LvlVM = new() { Stages = stages },
                subVm = new() { Stages = stages, Levels = levels },
                Stage = new()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateStage(Stage stage)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddNew");
            }

            context.Stages.Add(stage);
            context.SaveChanges();

            return View("Index");
        }

        [HttpPost]
        public IActionResult CreateLevel(AdminLevelVM vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddNew");
            }

            Level lvl = new()
            {
                StageID = vm.StageID,
                Name = vm.Name
            };

            context.Levels.Add(lvl);
            context.SaveChanges();

            return View("Index");      
        }

        [HttpPost]
        public IActionResult CreateSubject(AdminLvlSubjectVM vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddNew");
            }

            LevelSubject levelSubject = new()
            {
                StageID = vm.StageID,
                LevelID = vm.LevelID,
                Subject = new() { Name = vm.Name}
            };

            context.LevelsSubject.Add(levelSubject);
            context.SaveChanges();

            return View("Index");
        }

        [HttpPost]
        public IActionResult CureentStageLevel(int Id)
        {
            List<Level> lvls = context.Levels.Where(l => l.StageID == Id).ToList();

            return Json(lvls);
        }

        [HttpPost]
        public IActionResult GetStudents(bool IsDeleted)
        {
            int pgLength = int.Parse(Request.Form["length"]);
            int startPg = int.Parse(Request.Form["start"]);
            string searchVal = Request.Form["search[value]"];

            IQueryable<AdminUsersVM> students = context.Students
                .Where(s => s.AppUser.IsDeleted == IsDeleted)
                .Where(s => string.IsNullOrEmpty(searchVal)
                ? true
                : (s.AppUser.FirstName.Contains(searchVal)) || (s.AppUser.LastName.Contains(searchVal)) || (s.AppUser.Email.Contains(searchVal)))
                .Select(s => new AdminUsersVM
                {
                    Id = s.AppUserID,
                    FirstName = s.AppUser.FirstName,
                    LastName = s.AppUser.LastName,
                    Email = s.AppUser.Email,
                    JoinDate = s.AppUser.JoinDate,
                    IsDeleted = s.AppUser.IsDeleted
                });

            string orderCol = Request.Form[$"columns[{Request.Form["order[0][column]"]}][name]"];
            string orderDir = Request.Form["order[0][dir]"];

            if (!(string.IsNullOrWhiteSpace(orderCol) && string.IsNullOrEmpty(orderDir)))
            {
                students = students.OrderBy($"{orderCol} {orderDir}");
            }


            List<AdminUsersVM> data = students.Skip(startPg).Take(pgLength).ToList();

            int recordsTotal = students.Count();
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };
            return Json(jsonData);
        }

        [HttpPost]
        public IActionResult GetTeachers(bool IsDeleted)
        {
            int pgLength = int.Parse(Request.Form["length"]);
            int startPg = int.Parse(Request.Form["start"]);
            string searchVal = Request.Form["search[value]"];

            IQueryable<AdminUsersVM> teachers = context.Teachers
                .Where(t => t.AppUser.IsDeleted == IsDeleted)
                .Where(t => string.IsNullOrEmpty(searchVal)
                ? true
                : (t.AppUser.FirstName.Contains(searchVal)) || (t.AppUser.LastName.Contains(searchVal)) || (t.AppUser.Email.Contains(searchVal)))
                .Select(t => new AdminUsersVM
                {
                    Id = t.AppUserID,
                    FirstName = t.AppUser.FirstName,
                    LastName = t.AppUser.LastName,
                    Email = t.AppUser.Email,
                    JoinDate = t.AppUser.JoinDate,
                    IsDeleted = t.AppUser.IsDeleted
                });

            string orderCol = Request.Form[$"columns[{Request.Form["order[0][column]"]}][name]"];
            string orderDir = Request.Form["order[0][dir]"];

            if (!(string.IsNullOrWhiteSpace(orderCol) && string.IsNullOrEmpty(orderDir)))
            {
                teachers = teachers.OrderBy($"{orderCol} {orderDir}");
            }


            List<AdminUsersVM> data = teachers.Skip(startPg).Take(pgLength).ToList();

            int recordsTotal = teachers.Count();
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };
            return Json(jsonData);
        }

        [HttpPost]
        public IActionResult GetLessons(bool IsDeleted)
        {
            int pgLength = int.Parse(Request.Form["length"]);
            int startPg = int.Parse(Request.Form["start"]);
            string searchVal = Request.Form["search[value]"];

            IQueryable<AdminLessonVM> lessons = context.Lessons
                .Where(l => l.IsDeleted == IsDeleted)    
                .Where(l => string.IsNullOrEmpty(searchVal)
                ? true
                : (l.Title.Contains(searchVal)) || (l.Teacher.AppUser.FirstName.Contains(searchVal)) || (l.Subject.Name.Contains(searchVal)))
                .Select(l => new AdminLessonVM
                {
                    ID = l.ID,
                    Title = l.Title,
                    Price = l.Price,
                    Views = l.Views.Count,  //==> Or Query TO GET TOTAL VIEWS FROM HISTORY l.History.Count(),        
                    Orders = l.Orders.Count(),
                    PublishDate = l.PublishDate,
                }); 

            string orderCol = Request.Form[$"columns[{Request.Form["order[0][column]"]}][name]"];
            string orderDir = Request.Form["order[0][dir]"];

            if (!(string.IsNullOrWhiteSpace(orderCol) && string.IsNullOrEmpty(orderDir)))
            {
                lessons = lessons.OrderBy($"{orderCol} {orderDir}");
            }


            List<AdminLessonVM> data = lessons.Skip(startPg).Take(pgLength).ToList();

            int recordsTotal = lessons.Count();
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };
            return Json(jsonData);
        }

        [HttpPost]
        public IActionResult GetComments(bool IsDeleted)
        {
            int pgLength = int.Parse(Request.Form["length"]);
            int startPg = int.Parse(Request.Form["start"]);
            string searchVal = Request.Form["search[value]"];

            IQueryable<AdminCommentVM> comments = context.LessonComments
                .Where(c => c.IsDeleted == IsDeleted)
                .Where(c => string.IsNullOrEmpty(searchVal)
                ? true
                : (c.Lesson.Title.Contains(searchVal)) || (c.Student.AppUser.FirstName.Contains(searchVal)) || (c.Body.Contains(searchVal)))
                .Select(c => new AdminCommentVM
                {
                    ID = c.ID,
                    Student = c.Student.AppUser.FirstName,
                    LessonID = c.LessonID,
                    Lesson = c.Lesson.Title,                    //===> not needed        
                    Body = c.Body,
                    Date = c.Date,
                });

            string orderCol = Request.Form[$"columns[{Request.Form["order[0][column]"]}][name]"];
            string orderDir = Request.Form["order[0][dir]"];

            if (!(string.IsNullOrWhiteSpace(orderCol) && string.IsNullOrEmpty(orderDir)))
            {
                comments = comments.OrderBy($"{orderCol} {orderDir}");
            }


            List<AdminCommentVM> data = comments.Skip(startPg).Take(pgLength).ToList();

            int recordsTotal = comments.Count();
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };
            return Json(jsonData);
        }

        [HttpPost]
        public async Task<IActionResult> SuspendUser(string userId, [FromServices] UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.IsDeleted = true;
            await userManager.UpdateAsync(user);
            return Ok(new {id = userId});
        }

        [HttpPost]
        public async Task<IActionResult> UnSuspendUser(string userId, [FromServices] UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.IsDeleted = false;
            await userManager.UpdateAsync(user);
            return Ok(new { id = userId });
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteLesson(int lessonID)
        {
            Lesson lesson = await context.Lessons.FindAsync(lessonID);
            if (lesson == null)
            {
                return NotFound();
            }
            lesson.IsDeleted = true;
            await context.SaveChangesAsync();
            return Ok(new {id = lessonID});
        }

        [HttpPost]
        public async Task<IActionResult> RestoreLesson(int lessonID)
        {
            Lesson lesson = await context.Lessons.FindAsync(lessonID);
            if (lesson == null)
            {
                return NotFound();
            }
            lesson.IsDeleted = false;
            await context.SaveChangesAsync();
            return Ok(new { id = lessonID });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentID)
        {
            LessonComment comment = await context.LessonComments.FindAsync(commentID);
            if (comment == null)
            {
                return NotFound();
            }
            comment.IsDeleted = true;

            //context.LessonComments.Remove(comment);   ==> for Permenant DELETE

            await context.SaveChangesAsync();
            return Ok(new { id = commentID });
        }

    }
}
