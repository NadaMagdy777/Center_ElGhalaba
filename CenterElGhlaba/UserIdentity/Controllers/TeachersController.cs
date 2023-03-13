using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Center_ElGhlaba.Controllers
{
    public class TeachersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public TeachersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: TeachersController
        public async Task<ActionResult> Index()
        {
            return View(await unitOfWork.Teachers.GetAllAsync());
        }

        // GET: TeachersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View( await unitOfWork.Teachers.FindAsync(t => t.ID == id, new[] { "AppUser" }));
        }

        // GET: TeachersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeachersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Teachers.Insert(teacher);
                unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: TeachersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await unitOfWork.Teachers.FindAsync(t=>t.ID == id , new[] {"AppUser"}));
        }

        // POST: TeachersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Teacher UpdatedTeacher)
        {
            if (ModelState.IsValid)
            {
                Teacher OreginalTeacher = await unitOfWork.Teachers.FindAsync(t=>t.ID == id , new[] {"AppUser"});

                OreginalTeacher.AppUser.FirstName = UpdatedTeacher.AppUser.FirstName;
                
                //unitOfWork.Teachers.Insert(teacher);
                //unitOfWork.Complete();
                //return RedirectToAction("Index");
            }

            return View();
        }

        // GET: TeachersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeachersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
