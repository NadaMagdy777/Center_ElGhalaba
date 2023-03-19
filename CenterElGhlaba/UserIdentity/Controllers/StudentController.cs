using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.Services;
using Microsoft.AspNetCore.Mvc;

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
            const int pageSize = 1;


            int recentCount = lessons.Count();
            int recSkip = (pg - 1) * pageSize;
            var data = lessons.Skip(recSkip).Take(pageSize).ToList();
            return Json(data);
        }

        public async Task<IActionResult> Details(string id)
        {
            Student student = await services.GetStudent(id);

            List<Lesson> lessons = await services.GetStudentLessons(student.ID);
            const int pageSize = 1;

            int recentCount = lessons.Count();
            Pager pager = new Pager(recentCount, 1, pageSize);
            int recSkip = (1 - 1) * pageSize;
            this.ViewBag.Pager = pager;
            this.ViewBag.Lessons = lessons.Skip(recSkip).Take(pager.PageSize).ToList(); ;

            return View(student);


        }
    }
}
