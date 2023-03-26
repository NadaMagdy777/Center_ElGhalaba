using Center_ElGhalaba.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Center_ElGhlaba.ViewModels
{
	public class LessonVM
	{
		public int ID { get; set; }
        
		[MinLength(4, ErrorMessage = "Name Must be more than 3 letter")]
        [MaxLength(20, ErrorMessage = "Name Must be less than 10 letter")]
        public string Title { get; set; }
        [MinLength(10, ErrorMessage = "Description Must be more than 10 letter")]
        [MaxLength(50, ErrorMessage = "Description must be less than 50 letter")]
        public string Description { get; set; }
       
		[Range(minimum: 1, maximum: 2, ErrorMessage = "Duration Must be between 1 and 2")]

        public int Duration { get; set; }
        [Range(minimum: 20, maximum: 1000, ErrorMessage = "Price Must be between 20 and 100")]
        public decimal Price { get; set; }
       
		[Remote("CheckDiscount", "Lesson", AdditionalFields = "Price",
           ErrorMessage = "Discount Should be Less Than Lesson Price")]
        public decimal Discount { get; set; }
		[Required(ErrorMessage ="You should select Image")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "You should select Video")]

        public IFormFile VideoFile { get; set; }

		public List<IFormFile>? Resourses { get; set; }
		[ForeignKey("Subject")]
		public int subjectID { get; set; }

		[ForeignKey("Level")]
		public int levelID { get; set; }

		[ForeignKey("Teacher")]
		public int TeacherID { get; set; }

		[JsonIgnore]
		public virtual Subject? Subject { get; set; }
		[JsonIgnore]
		public virtual Level? Level { get; set; }
		[JsonIgnore]
		public virtual Teacher? Teacher { get; set; }
	}
}
