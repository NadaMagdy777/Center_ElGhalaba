using AutoMapper;
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
        private readonly IMapper _mapper;
        public LessonController(IUnitOfWork unitOfWork, IHostingEnvironment hosting, IMapper mapper)//ILessonService lessonService)
        {
            _UnitOfWork = unitOfWork;
            this.hosting = hosting;
            _mapper = mapper;
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
        public async Task<IActionResult> Details(int id, int userID)
        {
            Lesson lesson = await _UnitOfWork.Lessons.FindAsync(l => l.ID == id,
                new[] { "Teacher" });

            List<LessonComment> comments = await _UnitOfWork.comments
               .FindAllAsync(c => c.LessonID == id, new[] { "Student" });

            Student student = await _UnitOfWork.Students.FindAsync(s => s.ID == userID,
                new[] { "Orders" });


            var result = _mapper.Map<LessonDetailsVM>(lesson);
            _mapper.Map<LessonDetailsVM>(student);
            _mapper.Map<LessonDetailsVM>(comments);///////////////////

            return View(result);
        }
        public async Task<IActionResult> New(int TeacherId)
        { 
            ViewBag.TeacherId = TeacherId;
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

				_UnitOfWork.Lessons.Insert(lesson);
				_UnitOfWork.Complete();

                List<string> resourses = UploadsResoursesToFolder(newLesson.Resourses, "LessonsMaterial\\LessonResourses");
                insertResoursesDB(resourses, lesson.ID);


                return RedirectToAction("Index");

            }
			ViewBag.stages = await _UnitOfWork.stages.GetAllAsync();
			ViewBag.TeacherId=newLesson.TeacherID;

			return View(newLesson);



		}

        public string UploadsVideoToFolder(IFormFile File,string path)
        {
            string fileName = string.Empty;
			if (File != null)
			{
				string uploads = Path.Combine(hosting.WebRootPath,path );
				fileName = File.FileName;
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
                    resourse.Name = name;
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

                    resName = res.FileName;
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
    }
}

