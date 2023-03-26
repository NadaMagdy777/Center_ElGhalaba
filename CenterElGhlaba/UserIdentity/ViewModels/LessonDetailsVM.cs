using Center_ElGhalaba.Models;
using Center_ElGhlaba.Models;
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
        public List<LessonLikes>? Likes { get; set; }
        public List<LessonViews>? Views { get; set; }

        public int LikesCount { get; set; }
        public int ViewsCount { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Duration { get; set; }
        public Subject? Subject { get; set; }
        public Level? Level { get; set; }     
        public List<LessonComment>? Comments { get; set; }
        public List<LessonResource>? Resources { get; set; }
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
