using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
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
        public async Task<IActionResult> Details(int id)
        {

            return View(await _unitOfWork.Students.FindAsync(S => S.ID == id, new[] { "AppUser" }));
        }
    }
}
