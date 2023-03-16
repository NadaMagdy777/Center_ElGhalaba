using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.Models;
using Microsoft.AspNetCore.Mvc;

namespace Center_ElGhlaba.Controllers
{
    public class StudentController : Controller
    {
      
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetStudentLessons(int id, int pg)
        {
            var StudentHistory = await _unitOfWork.History.FindAllAsync(H => H.StudentID == id, new[] { "Lesson" });

            var lessons = StudentHistory.Select(L => L.Lesson).ToList();
            const int pageSize = 1;



            int recentCount = lessons.Count();
            int recSkip = (pg - 1) * pageSize;
            var data = lessons.Skip(recSkip).Take(pageSize).ToList();
            return Json(data);
        }

        public async Task<IActionResult> Details(string id)
        {
            Student student = await _unitOfWork.Students.FindAsync(S => S.AppUser.Id == id, new[] { "AppUser" });

            var StudentHistory = await _unitOfWork.History.FindAllAsync(H => H.StudentID == student.ID, new[] { "Lesson" });

            List<Lesson> lessons = StudentHistory.Select(L => L.Lesson).ToList();
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
