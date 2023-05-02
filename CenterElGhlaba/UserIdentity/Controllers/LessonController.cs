using AutoMapper;
using Center_ElGhalaba.Models;
using Center_ElGhlaba.Hubs;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.Models;
using Center_ElGhlaba.Services;
using Center_ElGhlaba.Unit_OfWork;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Center_ElGhlaba.Controllers
{
    public class LessonController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IHostingEnvironment hosting;
        private readonly IMapper _mapper;
        public IHubContext<LessonHub> LessonHub { get; }
        public IHubContext<LessonLikesHub> _LessonLikesHub { get; }
        

        public LessonController(IUnitOfWork unitOfWork, IHostingEnvironment hosting, IMapper mapper, IHubContext<LessonHub> _LessonHub, IHubContext<LessonLikesHub> LessonLikesHub)
        {
            _UnitOfWork = unitOfWork;
            this.hosting = hosting;
            _mapper = mapper;
            LessonHub = _LessonHub;
            _LessonLikesHub = LessonLikesHub;
        }
      
        public async Task<IActionResult> GetLessons(int pg)
        {

            var lessons = await _UnitOfWork.Lessons.GetAllAsync(new[] { "Likes", "Views" });
            const int pageSize = 6;

            int recentCount = lessons.Count();
            int recSkip = (pg - 1) * pageSize;
            var data = lessons.Skip(recSkip).Take(pageSize).ToList();
            return Json(data);
        }

        public async Task<IActionResult> Index()
        {
            const int pageSize = 6;
            List<Lesson> lessons = await _UnitOfWork.Lessons.GetAllAsync(new[] { "Likes", "Views" });

            int recentCount = lessons.Count();
            Pager pager = new Pager(recentCount, 1, pageSize);
            int recSkip = (1 - 1) * pageSize;
            this.ViewBag.Pager = pager;

            return View(lessons.Skip(recSkip).Take(pager.PageSize).ToList());
        }
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Watch(int id, string userID)
        {
            Lesson lesson = await _UnitOfWork.Lessons.FindAsync(l => l.ID == id, new[] { "Teacher.AppUser", "Subject", "Level", "Comments.Student.AppUser", "Likes", "Views" , "Resources" });
            Student student = await _UnitOfWork.Students.FindAsync(s => s.AppUserID == userID,new[] { "Orders.Lesson" , "AppUser" });

           
            var result = _mapper.Map<LessonDetailsVM>(lesson);
                         _mapper.Map<LessonDetailsVM>(student);

            return View(result);
        }
        public async Task<IActionResult> Details(int id, string? userID)
        {
            Lesson lesson = await _UnitOfWork.Lessons.FindAsync(l => l.ID == id, new[] { "Teacher.AppUser", "Subject", "Level" , "Comments.Student.AppUser", "Likes", "Views"});
            var result = _mapper.Map<LessonDetailsVM>(lesson);

            if (userID != null)
            {
                Student student = await _UnitOfWork.Students.FindAsync(s => s.AppUserID == userID, new[] { "Orders.Lesson" , "AppUser" });
                _mapper.Map<LessonDetailsVM>(student);
            }

            return View(result);
        }
        public async Task<ActionResult> IsLike(int lessonId, string studentId)
        {
            Student student = await _UnitOfWork.Students.FindAsync(s => s.AppUserID == studentId);
            Lesson lesson = await _UnitOfWork.Lessons.FindAsync(l => l.ID == lessonId, new[] { "Likes" });
            LessonLikes HasLike = lesson.Likes.FirstOrDefault(k => k.LessonId == lessonId && k.StudentId == student.ID);
            if (HasLike != null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        public async Task AddLike(int lessonId, string studentId)
        {

            Student student = await _UnitOfWork.Students.FindAsync(s => s.AppUserID == studentId);
            Lesson lesson = await _UnitOfWork.Lessons.FindAsync(l => l.ID == lessonId, new[] { "Likes" });
            lesson.Likes.Add(new LessonLikes { StudentId = student.ID, LessonId = lessonId });
            _UnitOfWork.Complete();

            await _LessonLikesHub.Clients.All.SendAsync("AddLessonLike", lessonId);
        }
        public async Task RemoveLike(int lessonId, string studentId)
        {
            Student student = await _UnitOfWork.Students.FindAsync(s => s.AppUserID == studentId);
            Lesson lesson = await _UnitOfWork.Lessons.FindAsync(l => l.ID == lessonId, new[] { "Likes" });

            LessonLikes HasLike = lesson.Likes.FirstOrDefault(k => k.StudentId == student.ID && k.LessonId == lesson.ID);
            if (HasLike != null)
            {
                lesson.Likes.Remove(HasLike);
                _UnitOfWork.Complete();

                await _LessonLikesHub.Clients.All.SendAsync("RemoveLessonLike", lessonId);
            }

        }
 
        public async Task<ActionResult> IsViewed(int lessonId, string studentId)
        {
            Student student = await _UnitOfWork.Students.FindAsync(s => s.AppUserID == studentId);
            Lesson lesson = await _UnitOfWork.Lessons.FindAsync(l => l.ID == lessonId, new[] { "Views" });
            LessonViews isViewed = lesson.Views.FirstOrDefault(k => k.LessonId == lessonId && k.StudentId == student.ID);
            if (isViewed != null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        public async Task AddView(int lessonId, string studentId)
        {

            Student student = await _UnitOfWork.Students.FindAsync(s => s.AppUserID == studentId);
            Lesson lesson = await _UnitOfWork.Lessons.FindAsync(l => l.ID == lessonId, new[] { "Views" });
            lesson.Views.Add(new LessonViews { StudentId = student.ID, LessonId = lessonId });
            _UnitOfWork.Complete();

            await _LessonLikesHub.Clients.All.SendAsync("AddLessonView", lessonId);
        }

        public async Task<ActionResult> SubjectLessons(int id)
        {
            const int pageSize = 6;
            List<Lesson> lessons = await _UnitOfWork.Lessons.FindAllAsync(l => l.subjectID == id, new[] { "Likes", "Views" });

            int recentCount = lessons.Count();
            Pager pager = new Pager(recentCount, 1, pageSize);
            int recSkip = (1 - 1) * pageSize;
            this.ViewBag.Pager = pager;

            this.ViewBag.subjectLesson = true;
            return View("Index", lessons.Skip(recSkip).Take(pager.PageSize).ToList());

        }
        //From Moeen
        public async Task<ActionResult> TeacherLessons(string id)
        {
            Teacher teacher = await _UnitOfWork.Teachers.FindAsync(t => t.AppUserID == id);
            List<Lesson> lessons = await _UnitOfWork.Lessons.FindAllAsync(l => l.TeacherID == teacher.ID, new[] {"Likes","Views"});
            return View("Index", lessons);
        }
        public async Task<ActionResult> TeacherNew(string id)
        {
            Teacher teacher = await _UnitOfWork.Teachers.FindAsync(t => t.AppUserID == id);
            ViewBag.TeacherId = teacher.ID;
            ViewBag.stages = await _UnitOfWork.stages.GetAllAsync();
            return View("New");
        }
        public async Task<IActionResult> New(int id)
        {
            ViewBag.TeacherId = id;
            ViewBag.stages = await _UnitOfWork.stages.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(LessonVM newLesson)
        {
            if (ModelState.IsValid)
            {
				
				Lesson lesson = new Lesson();

                lesson.CoverPicture =await ConvertImageToByte(newLesson.ImageFile);

				lesson.subjectID = newLesson.subjectID;
                lesson.FilePath = UploadsVideoToFolder(newLesson.VideoFile, "LessonsMaterial\\LessonVideos");
				lesson.Duration = newLesson.Duration;
				lesson.Description = newLesson.Description;
				lesson.Title = newLesson.Title;
				lesson.Discount = newLesson.Discount;
				lesson.levelID = newLesson.levelID;
				lesson.TeacherID = newLesson.TeacherID;
                lesson.Price = newLesson.Price;
                lesson.PublishDate = DateTime.Now;

				_UnitOfWork.Lessons.Insert(lesson);
				_UnitOfWork.Complete();

                List<string> resourses = UploadsResoursesToFolder(newLesson.Resourses, "LessonsMaterial\\LessonResourses");
                insertResoursesDB(resourses, lesson.ID);

               
              
                Teacher teacher = await _UnitOfWork.Teachers.FindAsync(T => T.ID == lesson.TeacherID,new[] { "AppUser" });

                await LessonHub.Clients.Group(lesson.TeacherID.ToString()).SendAsync("NewLessonAdded", lesson, teacher.AppUser.FirstName +" "+teacher.AppUser.LastName);

                return RedirectToAction("Index");

            }
			ViewBag.stages = await _UnitOfWork.stages.GetAllAsync();
			ViewBag.TeacherId=newLesson.TeacherID;

			return View(newLesson);
		}
        public async Task<IActionResult> Edit(int id)
        {

            Lesson lesson =await _UnitOfWork.Lessons.GetByIdAsync(id);
            EditLessonVM Lesson = new EditLessonVM();
            Lesson.Title = lesson.Title;
            Lesson.Price = lesson.Price;
            Lesson.Description = lesson.Description;
            Lesson.Discount= lesson.Discount;
            Lesson.ID=lesson.ID;
            
            return View(Lesson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditLessonVM lesson)
        {
            if (ModelState.IsValid)
            {
                Lesson Lesson = await _UnitOfWork.Lessons.FindAsync(l => l.ID == lesson.ID, new[] { "Teacher" }) ;

                Lesson.Title = lesson.Title;
                Lesson.Price = lesson.Price;
                Lesson.Description = Lesson.Description;
                Lesson.Discount = lesson.Discount;
                Lesson.ID = lesson.ID;

                _UnitOfWork.Lessons.Update(Lesson);
                _UnitOfWork.Complete();

                return RedirectToAction("Details", "Teachers", new { id = Lesson.Teacher.AppUserID});
            }

            return View(lesson);
        }

        public string UploadsVideoToFolder(IFormFile File,string path)
        {
            string fileName = string.Empty;
			if (File != null)
			{
				string uploads = Path.Combine(hosting.WebRootPath,path );
				fileName = Guid.NewGuid().ToString() + File.FileName;
				string fullpath = Path.Combine(uploads, fileName);
				using var fileStream = new FileStream(fullpath, FileMode.Create);

				File.CopyTo(fileStream);
				fileStream.Close();
			}

			return fileName;
        }

        public void insertResoursesDB(List<string> resourses,int lessonId)
        {
          
            if (resourses != null)
            {
                foreach(string name in resourses)
                {
                    LessonResource resourse = new LessonResource();
                    resourse.LessonID = lessonId;
                    resourse.Name = name.Substring(36);
                    resourse.Value = name;
                    _UnitOfWork.resources.Insert(resourse);
                    _UnitOfWork.Complete();

                }
            }
        }
        public List<string> UploadsResoursesToFolder(List<IFormFile> Files, string path)
        {
            List<string> filesname = new List<string>();
            string fileName = string.Empty;
            if ( Files!= null)
            {
                string resName = string.Empty;
                string uploads = Path.Combine(hosting.WebRootPath,path);
                foreach (var res in Files)
                {
                    resName = Guid.NewGuid().ToString() + res.FileName;
                    filesname.Add(resName);
                    string fullpath = Path.Combine(uploads, resName);
                    using var ResStream = new FileStream(fullpath, FileMode.Create);
                    res.CopyTo(ResStream);
                    ResStream.Close();

                }
            }

            return filesname;
        }
        public async Task<byte[]> ConvertImageToByte(IFormFile imgFile)
        {
            byte[] image= null;
            if (imgFile != null)
            {
                using (var dataStream = new MemoryStream())
                {
                    await imgFile.CopyToAsync(dataStream);
                    image = dataStream.ToArray();
                    dataStream.Close();
                }
            }
            return image;
        }
        public async Task<IActionResult> GetLevels(int StageID)
        {
            var levels = await _UnitOfWork.levels.FindAllAsync(item => item.StageID == StageID);
            return Json(levels);
        }
        public async Task<IActionResult> GetSubjects(int LevelID, int StageID)
        {
            var levelsubjects = await _UnitOfWork.levelSubjects.FindAllAsync(item => item.StageID == StageID && item.LevelID == LevelID, new[] { "Subject" });
            var subjects = levelsubjects.Select(item => item.Subject);
            return Json(subjects);
        }
        public IActionResult CheckDiscount(int Discount, int Price)
        {
            if (Discount < Price)
            {
                return Json(true);
            }
            else
                return Json(false);
        }
    }
}

