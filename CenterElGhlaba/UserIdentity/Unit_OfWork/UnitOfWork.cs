using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.Repositories;
using UserIdentity.Data;

namespace Center_ElGhlaba.Unit_OfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<Lesson> Lessons { get; private set; }
        public IBaseRepository<Student> Students { get; private set; }
        public IBaseRepository<Teacher> Teachers { get; private set; }
        public IBaseRepository<StudentHistory> History { get; private set; }
        public IBaseRepository<StudentOrder> Orders { get; private set; }
        public IBaseRepository<LessonComment> comments { get; private set; }

        public IBaseRepository<Stage> stages { get; private set; }
        public IBaseRepository<Level> levels { get; private set; }
        public IBaseRepository<Subject> subjects { get; private set; }

        public IBaseRepository<LevelSubject> levelSubjects { get; private set; }
        public IBaseRepository<LessonResource> resources { get; private set; }
        public IBaseRepository<TeacherPaymentMethod> TeacherPaymentMethod { get; private set; }
        public IBaseRepository<TeacherLogs> TeacherLogs { get; private set; }
        public IBaseRepository<Follows> Follows { get; private set; }


        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context;

            Lessons = new BaseRepository<Lesson>(_context);
            Students = new BaseRepository<Student>(_context);
            Teachers = new BaseRepository<Teacher>(_context);
            History = new BaseRepository<StudentHistory>(_context);
            Orders = new BaseRepository<StudentOrder>(_context);
            comments = new BaseRepository<LessonComment>(_context);
            subjects = new BaseRepository<Subject>(_context);
            levels = new BaseRepository<Level>(_context);
            stages = new BaseRepository<Stage>(_context);
            levelSubjects = new BaseRepository<LevelSubject>(_context);
            resources= new BaseRepository<LessonResource>(_context);
            TeacherPaymentMethod = new BaseRepository<TeacherPaymentMethod>(_context);
            TeacherLogs = new BaseRepository<TeacherLogs>(_context);
            Follows = new BaseRepository<Follows>(_context);
        }

        public UnitOfWork()
        {
        }

        public int Complete() 
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
