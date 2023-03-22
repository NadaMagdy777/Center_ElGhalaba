using Center_ElGhalaba.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Center_ElGhlaba.ViewModels
{
	public class LessonVM
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Duration { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; }       

        public IFormFile ImageFile { get; set; }
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
