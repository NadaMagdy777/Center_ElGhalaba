using Center_ElGhalaba.Models;

namespace Center_ElGhlaba.ViewModels
{
    public class AdminStudentsVM
    {
        public List<Student> ActiveStudents { get; set; }
        public List<Student> DeletedStudents { get; set; }
    }
}
