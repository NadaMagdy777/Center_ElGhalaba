using Center_ElGhalaba.Constants;
using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.Services;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserIdentity.Models;

namespace Center_ElGhlaba.Controllers
{
    public class StudentController : Controller
    {
      
        private readonly StudentServices services;

        public StudentController(StudentServices services)
        {
            this.services = services;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetStudentLessons(int id, int pg)
        {            

            List<Lesson> lessons =await services.GetStudentLessons(id);
            const int pageSize = 6;


            int recentCount = lessons.Count();
            int recSkip = (pg - 1) * pageSize;
            var data = lessons.Skip(recSkip).Take(pageSize).ToList();
            return Json(data);
        }

        [Authorize]
        public async Task<IActionResult> Details(string id,[FromServices] UserManager<ApplicationUser> userManager)
        {
            ApplicationUser currUser = await userManager.FindByNameAsync(User.Identity.Name);

            if (id != currUser.Id)
            {
                if ( await userManager.IsInRoleAsync(currUser, RolesConsts.Student.ToString()))
                {
                    return RedirectToAction("Details", new { id = currUser.Id });
                }
                else if (await userManager.IsInRoleAsync(currUser, RolesConsts.Teacher.ToString()))
                {
                    return RedirectToAction("Details", "Teachers", new { id = currUser.Id });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            Student student = await services.GetStudent(id);

            List<Lesson> lessons = await services.GetStudentLessons(student.ID);
            const int pageSize = 6;

            int recentCount = lessons.Count();
            Pager pager = new Pager(recentCount, 1, pageSize);
            int recSkip = (1 - 1) * pageSize;
            this.ViewBag.Pager = pager;
           
            this.ViewBag.Lessons = lessons.Skip(recSkip).Take(pager.PageSize).ToList(); ;

            return View(student);
        }
    }
}
