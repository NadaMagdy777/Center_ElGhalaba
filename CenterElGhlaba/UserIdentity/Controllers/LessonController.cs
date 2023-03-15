using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.Services;
using Center_ElGhlaba.Unit_OfWork;
using Microsoft.AspNetCore.Mvc;

namespace Center_ElGhlaba.Controllers
{
    public class LessonController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        //private ILessonService _Service;

        //public LessonController()
        //{
        //    _Service = new LessonService(new ModelStateWrapper(this.ModelState), new UnitOfWork());

        //}
        public LessonController(IUnitOfWork unitOfWork)//ILessonService lessonService)
        {
            _UnitOfWork = unitOfWork;
            //_Service = lessonService;
        }
        public async Task<ActionResult> Index()
        {           
            return View(await _UnitOfWork.Lessons.GetAllAsync());
        }
        //public IActionResult Details()
        //{

        //}
    }
}
