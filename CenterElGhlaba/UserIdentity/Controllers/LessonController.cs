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
            if (ModelState.IsValid)
            {
				string fileName = string.Empty;

				Lesson lesson = new Lesson();
				if (newLesson.VideoFile != null)
				{
					string uploads = Path.Combine(hosting.WebRootPath, "LessonsMaterial\\LessonVideos");
					fileName = newLesson.VideoFile.FileName;
					string fullpath = Path.Combine(uploads, fileName);
					using var fileStream = new FileStream(fullpath, FileMode.Create);

					newLesson.VideoFile.CopyTo(fileStream);
                    fileStream.Close();

				}
                

                if (newLesson.Resourses != null)
                {
                    string resName = string.Empty;
                    string uploads = Path.Combine(hosting.WebRootPath, "LessonsMaterial\\LessonResourses");
                    foreach (var res in newLesson.Resourses)
                    {
                        
                        resName = res.FileName;
                        string fullpath = Path.Combine(uploads, resName);
                        using var ResStream = new FileStream(fullpath, FileMode.Create);
                        res.CopyTo(ResStream);
                        ResStream.Close();

                    }
                   

                   

                }



                if (newLesson.ImageFile != null)
				{


					using (var dataStream = new MemoryStream())
					{
						await newLesson.ImageFile.CopyToAsync(dataStream);
						lesson.CoverPicture = dataStream.ToArray();
						dataStream.Close();
					}


				}

				lesson.subjectID = newLesson.subjectID;
                lesson.FilePath = fileName;
				lesson.Duration = newLesson.Duration;
				lesson.Description = newLesson.Description;
				lesson.Title = newLesson.Title;
				lesson.Discount = newLesson.Discount;
				lesson.levelID = newLesson.levelID;
				lesson.TeacherID = newLesson.TeacherID;
				_UnitOfWork.Lessons.Insert(lesson);
				_UnitOfWork.Complete();





				

			}
			ViewBag.stages = await _UnitOfWork.stages.GetAllAsync();
			ViewBag.TeacherId=newLesson.TeacherID;

			return View(newLesson);



		}

        public async Task<string> ConvertFileToFolder(IFormFile File,string path)
        {
            string fileName = string.Empty;
			if (File != null)
			{
				string uploads = Path.Combine(hosting.WebRootPath,path );
				fileName = File.FileName;
				string fullpath = Path.Combine(uploads, fileName);
				using var fileStream = new FileStream(fullpath, FileMode.Create);

				File.CopyToAsync(fileStream);
				fileStream.Close();

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

