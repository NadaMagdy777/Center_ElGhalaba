using Center_ElGhalaba.Models;

namespace Center_ElGhlaba.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IBaseRepository<Lesson> Lessons { get; }
        IBaseRepository<Student> Students { get; }
        IBaseRepository<Teacher> Teachers { get; }
        IBaseRepository<StudentHistory> History { get; }
        IBaseRepository<StudentOrder> Orders { get;}
        IBaseRepository<LessonComment> comments { get;}
        IBaseRepository<LessonResource> resources { get; }
        IBaseRepository<Stage> stages { get; }
        IBaseRepository<Level> levels { get; }
        IBaseRepository<Subject> subjects { get; }
        IBaseRepository<LevelSubject> levelSubjects { get; }
        IBaseRepository<TeacherPaymentMethod> TeacherPaymentMethod { get; }
        IBaseRepository<TeacherLogs> TeacherLogs { get; }
        IBaseRepository<Follows> Follows { get; }
        int Complete();
    }
}
