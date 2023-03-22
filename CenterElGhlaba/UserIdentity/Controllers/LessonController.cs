using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.Services;
using Center_ElGhlaba.Unit_OfWork;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Center_ElGhlaba.Controllers
{
    public class LessonController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IHostingEnvironment hosting;

        public LessonController(IUnitOfWork unitOfWork, IHostingEnvironment hosting)//ILessonService lessonService)
        {
            _UnitOfWork = unitOfWork;
            this.hosting = hosting;
            //_Service = lessonService;
        }

        //private ILessonService _Service;

        //public LessonController()
        //{
        //    _Service = new LessonService(new ModelStateWrapper(this.ModelState), new UnitOfWork());

        //}
      
        public async Task<ActionResult> Index()
        {           
            return View(await _UnitOfWork.Lessons.GetAllAsync());
        }
        //public IActionResult Details()
        //{

        //}
        public async Task<IActionResult> New(int TeacherId)
        {
            ViewBag.TeacherId = TeacherId;
            ViewBag.stages = await _UnitOfWork.stages.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> New(LessonVM newLesson)
        {
            

            string fileName = string.Empty;
            Lesson lesson = new Lesson();

            if (newLesson.ImageFile!= null)
            {
                
                fileName = newLesson.ImageFile.FileName;
              
                using (var dataStream = new MemoryStream())
                {
                    await newLesson.ImageFile.CopyToAsync(dataStream);
                    lesson.CoverPicture = dataStream.ToArray();
                }


            }
            
            lesson.subjectID = 1;
            lesson.FilePath = await ConvertFileToFolder(newLesson.VideoFile,"LessonsMaterial/LessonVideos");
            lesson.Duration = newLesson.Duration;
            lesson.Description = newLesson.Description;
            lesson.Title = newLesson.Title;
            lesson.Discount = newLesson.Discount;
            lesson.levelID = 1;
            lesson.TeacherID = newLesson.TeacherID;
            _UnitOfWork.Lessons.Insert(lesson);
            _UnitOfWork.Complete();





            return View(newLesson);
        }

        public async Task<string> ConvertFileToFolder(IFormFile File,string path)
        {
            string fileName = string.Empty;
            if (File != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, path);
                 fileName = File.FileName;
                string fullpath = Path.Combine(uploads, fileName);
                using var fileStream = new FileStream(fullpath, FileMode.Create);

                File.CopyToAsync(fileStream);

            }

            return fileName;
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
    }
}

