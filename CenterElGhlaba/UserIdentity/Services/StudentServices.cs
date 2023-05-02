using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.Unit_OfWork;

namespace Center_ElGhlaba.Services
{
    public class StudentServices
    {
        private readonly IUnitOfWork unit;

        public StudentServices(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<Student> GetStudent(string id)
        {
            return await unit.Students.FindAsync(s => !s.AppUser.IsDeleted && s.AppUser.Id == id, new string[] { "AppUser" });

        }
        public async Task<List<Lesson>> GetStudentLessons(int id)
        {
            var orders= await unit.Orders.FindAllAsync(O => O.StudentID == id, new[] { "Lesson.Views", "Lesson.Likes" }) ;
            return orders.Select(L => L.Lesson).ToList();

        }
    }
}
