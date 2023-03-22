using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;

namespace Center_ElGhlaba.Services
{
    public class AdminServices
    {
        private readonly IUnitOfWork unit;

        public AdminServices(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public async Task<IEnumerable<Student>> GetActiveStudents()
        {
            return await unit.Students.FindAllAsync(s => !s.AppUser.IsDeleted, new string[] { "AppUser" });
        }
        
        public async Task<IEnumerable<Teacher>> GetActiveTeachers()
        {
            return await unit.Teachers.FindAllAsync(t => !t.AppUser.IsDeleted, new string[] { "AppUser" });
        }
        
        public async Task<IEnumerable<Lesson>> GetActiveLessons()
        {
            return await unit.Lessons.FindAllAsync(l => !l.IsDeleted);
        }
        
        public async Task<IEnumerable<LessonComment>> GetActiveComments()
        {
            return await unit.comments.FindAllAsync(c => !c.IsDeleted);
        }
        
        public async Task<IEnumerable<Student>> GetDeletedStudents()
        {
            return await unit.Students.FindAllAsync(s => s.AppUser.IsDeleted, new string[] { "AppUser" });
        }
        
        public async Task<IEnumerable<Teacher>> GetDeletedTeachers()
        {
            return await unit.Teachers.FindAllAsync(t => t.AppUser.IsDeleted, new string[] { "AppUser" });
        }
        
        public async Task<IEnumerable<Lesson>> GetDeletedLessons()
        {
            return await unit.Lessons.FindAllAsync(l => l.IsDeleted);
        }
        
        public async Task<IEnumerable<LessonComment>> GetDeletedComments()
        {
            return await unit.comments.FindAllAsync(c => c.IsDeleted);
        }

        public async Task<string> DeleteStudent (string Id)
        {
            Student std = await unit.Students.FindAsync(s => s.AppUserID == Id, new string[] { "AppUser" });
            std.AppUser.IsDeleted = true;

            unit.Complete();
            return std.AppUserID;
        }
        
        public async Task<string> DeleteTeacher (string Id)
        {
            Teacher teacher = await unit.Teachers.FindAsync(t => t.AppUserID == Id, new string[] { "AppUser" });
            teacher.AppUser.IsDeleted = true;

            unit.Complete();
            return teacher.AppUserID;
        }
        
        public async Task<int> DeleteLesson (int Id)
        {
            Lesson lesson = await unit.Lessons.GetByIdAsync(Id);
            lesson.IsDeleted = true;

            unit.Complete();
            return lesson.ID;
        }
        
        public async Task<int> DeleteComment (int Id)
        {
            LessonComment comment = await unit.comments.GetByIdAsync(Id);
            comment.IsDeleted = true;

            unit.Complete();
            return comment.ID;
        }

        public Student EditStudent(Student std)
        {
            unit.Students.Update(std);
            unit.Complete();

            return std;
        }
        
        public Teacher EditTeacher(Teacher teacher)
        {
            unit.Teachers.Update(teacher);
            unit.Complete();

            return teacher;
        }
        
        public Lesson EditLesson(Lesson lesson)
        {
            unit.Lessons.Update(lesson);
            unit.Complete();

            return lesson;
        }
        
        public LessonComment EditComment(LessonComment comment)
        {
            unit.comments.Update(comment);
            unit.Complete();

            return comment;
        }
    }
}
