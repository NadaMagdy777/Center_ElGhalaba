using Center_ElGhalaba.Constants;
using Center_ElGhlaba.Services;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;

namespace Center_ElGhlaba.Controllers
{
    [Authorize (Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AdminServices services;

        public AdminController(AdminServices services)
        {
            this.services = services;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Students()
        {
            AdminStudentsVM vm = new()
            {
                ActiveStudents = services.GetActiveStudents().Result.ToList(),
                DeletedStudents = services.GetDeletedStudents().Result.ToList(),
            };

            return View(vm);
        }

        public async Task<IActionResult> SuspendStd(string Id)
        {
            return Json(await services.DeleteStudent(Id));
        }

        public IActionResult EditStudent(string Id)
        {
            return View();
        }
    }
}
