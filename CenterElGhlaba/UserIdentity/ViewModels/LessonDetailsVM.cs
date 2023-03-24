using Center_ElGhalaba.Models;
using System.ComponentModel.DataAnnotations;

namespace Center_ElGhlaba.ViewModels
{
    public class LessonDetailsVM
    {

        #region Lesson
        public int LessonID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public byte[] CoverPicture { get; set; }
        public DateTime PublishDate { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public List<LessonComment> Comments { get; set; }
        #endregion

        #region Teacher 
        public int ID { get; set; }
        [Required, MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }
        public byte[]? Image { get; set; }
        #endregion

        #region Student   
       
        public int studentID { get; set; }
        public string studentName { get; set; }
        public List<Lesson>? Orders { get; set; }
        #endregion

        public bool? IsOwnedBy { get; set; }
    }
}
