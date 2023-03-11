using Center_ElGhalaba.Models;

namespace Center_ElGhlaba.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IBaseRepository<Lesson> Lessons { get; }
        IBaseRepository<Student> Students { get; }
        IBaseRepository<Teacher> Teachers { get; }
        int Complete();
    }
}
