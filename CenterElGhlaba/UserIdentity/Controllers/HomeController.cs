using Center_ElGhalaba.Constants;
using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserIdentity.Models;

namespace UserIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unit;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unit, ILogger<HomeController> logger)
        {
            this.unit = unit;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();

            List<Teacher> teacherModel = await unit.Teachers            //////////Throw Exception ===> Likes
                .FindAllAsync(t => true, new string[] { "AppUser", "Likes" }, t => t.Lessons.SelectMany(l => l.Orders).Count(), OrderBy.Descending);

            List<Subject> subjectModel = unit.subjects.GetAllAsync().Result.Take(8).ToList();

            List<Lesson> NewAddedlessonModel = unit.Lessons
                .FindAllAsync(l => true, null, l => l.PublishDate, OrderBy.Descending).Result.Take(3).ToList();


            List<Lesson> HighViewslessonModel = unit.Lessons
                .FindAllAsync(l => true, new[] {"Views" , "Likes"} , l => l.Views.Count, OrderBy.Descending).Result.Take(3).ToList();

            indexViewModel.Teacherslist = teacherModel == null? new List<Teacher>() : teacherModel;
            indexViewModel.Subjectslist = subjectModel == null? new List<Subject>() : subjectModel;
            indexViewModel.higestLessonslistviews = HighViewslessonModel == null ? new List<Lesson>() : HighViewslessonModel;
            indexViewModel.NewLessonslistadded = NewAddedlessonModel == null ? new List<Lesson>() : NewAddedlessonModel;

            return View(indexViewModel);
        }
     

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}