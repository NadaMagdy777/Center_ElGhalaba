using Center_ElGhalaba.Models;
using Center_ElGhlaba.Hubs;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;

namespace Center_ElGhlaba.Controllers
{
    public class TeachersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public IHubContext<TeacherHub> Teacherhub { get; }

        public TeachersController(IUnitOfWork unitOfWork, IHubContext<TeacherHub> _Teacherhub)
        {
            this.unitOfWork = unitOfWork;
            Teacherhub = _Teacherhub;
        }

        // GET: TeachersController/Details/5
        [Authorize]
        public async Task<ActionResult> Details(string id)
        {
            return View(await unitOfWork.Teachers.FindAsync(t => t.AppUserID == id, new[] { "AppUser", "Lessons", "Follows", "Likes" }));
        }

        public async Task<ActionResult> IsFolowwing(string studentId,string teacherId)
        {
            Student student = await unitOfWork.Students.FindAsync(s => s.AppUserID == studentId);
            Teacher teacher = await unitOfWork.Teachers.FindAsync(t => t.AppUserID == teacherId, new[] { "Follows" });
            Follows isfolow = teacher.Follows.FirstOrDefault(f => f.StudentID == student.ID && f.TeacherID == teacher.ID);
            if(isfolow != null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        public async Task AddFollower(string studentId, string teacherId)
        {

            Student student = await unitOfWork.Students.FindAsync(s => s.AppUserID == studentId);
            Teacher teacher = await unitOfWork.Teachers.FindAsync(t => t.AppUserID == teacherId, new[] { "Follows" });
            teacher.Follows.Add(new Follows { StudentID = student.ID, TeacherID = teacher.ID });
            unitOfWork.Complete();

            await Teacherhub.Clients.Users(teacherId).SendAsync("AddFollower");
        }
        public async Task RemoveFollower(string studentId, string teacherId)
        {
            Student student = await unitOfWork.Students.FindAsync(s => s.AppUserID == studentId);
            Teacher teacher = await unitOfWork.Teachers.FindAsync(t => t.AppUserID == teacherId, new[] { "Follows" });
            Follows isfolow = teacher.Follows.FirstOrDefault(f => f.StudentID == student.ID && f.TeacherID == teacher.ID);
            if (isfolow != null)
            {
                teacher.Follows.Remove(isfolow);
                unitOfWork.Complete();

                await Teacherhub.Clients.Users(teacherId).SendAsync("RemoveFollower");
            }

        }

        public async Task<ActionResult> IsLike(string studentId, string teacherId)
        {
            Student student = await unitOfWork.Students.FindAsync(s => s.AppUserID == studentId);
            Teacher teacher = await unitOfWork.Teachers.FindAsync(t => t.AppUserID == teacherId, new[] { "Likes" });
            Likes HasLike = teacher.Likes.FirstOrDefault(f => f.StudentID == student.ID && f.TeacherID == teacher.ID);
            if (HasLike != null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        public async Task AddLike(string studentId, string teacherId)
        {

            Student student = await unitOfWork.Students.FindAsync(s => s.AppUserID == studentId);
            Teacher teacher = await unitOfWork.Teachers.FindAsync(t => t.AppUserID == teacherId, new[] { "Likes" });
            teacher.Likes.Add(new Likes { StudentID = student.ID, TeacherID = teacher.ID });
            unitOfWork.Complete();

            await Teacherhub.Clients.Users(teacherId).SendAsync("AddLike");
        }
        public async Task RemoveLike(string studentId, string teacherId)
        {
            Student student = await unitOfWork.Students.FindAsync(s => s.AppUserID == studentId);
            Teacher teacher = await unitOfWork.Teachers.FindAsync(t => t.AppUserID == teacherId, new[] { "Likes" });
            Likes HasLike = teacher.Likes.FirstOrDefault(f => f.StudentID == student.ID && f.TeacherID == teacher.ID);
            if (HasLike != null)
            {
                teacher.Likes.Remove(HasLike);
                unitOfWork.Complete();

                await Teacherhub.Clients.Users(teacherId).SendAsync("RemoveLike");
            }

        }

        //// GET: TeachersController/Details/5
        //[Authorize(Roles = "Teacher")]
        //public ActionResult AddCourse(int id)
        //{
        //    return View();
        //}















        //// GET: TeachersController
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> Index()
        //{
        //    return View(await unitOfWork.Teachers.GetAllAsync());
        //}



        //// GET: TeachersController/Create
        //[Authorize(Roles ="Admin")]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: TeachersController/Create
        //[Authorize(Roles ="Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Teacher teacher)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        unitOfWork.Teachers.Insert(teacher);
        //        unitOfWork.Complete();
        //        return RedirectToAction("Index");
        //    }

        //    return View(teacher);
        //}

        //// GET: TeachersController/Edit/5
        //[Authorize(Roles ="Teacher,Admin")]
        //public async Task<ActionResult> Edit(int id)
        //{
        //    return View(await unitOfWork.Teachers.FindAsync(t=>t.ID == id , new[] {"AppUser"}));
        //}

        //// POST: TeachersController/Edit/5
        //[Authorize(Roles ="Teacher,Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(int id, Teacher UpdatedTeacher)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Teacher OreginalTeacher = await unitOfWork.Teachers.FindAsync(t=>t.ID == id , new[] {"AppUser"});

        //        OreginalTeacher.AppUser.FirstName = UpdatedTeacher.AppUser.FirstName;

        //        //unitOfWork.Teachers.Insert(teacher);
        //        //unitOfWork.Complete();
        //        //return RedirectToAction("Index");
        //    }

        //    return View();
        //}

        //// GET: TeachersController/Delete/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: TeachersController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
