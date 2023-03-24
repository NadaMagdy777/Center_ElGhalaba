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
        public bool IsDeleted { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Duration { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual Level? Level { get; set; }     
        public List<LessonComment> Comments { get; set; }
        #endregion

        #region Teacher 
        public virtual Teacher? Teacher { get; set; }

        #endregion

        #region Student   
        public Student student { get; set; }
        //public List<StudentOrder> Orders { get; set; }
        #endregion

        
    }
}
