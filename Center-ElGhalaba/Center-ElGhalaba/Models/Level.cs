using System.ComponentModel.DataAnnotations.Schema;

namespace Center_ElGhalaba.Models
{
    public class Level
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [ForeignKey("Stage")]
        public int StageID { get; set; }
        public virtual Stage? Stage { get; set; }
		public virtual List<Lesson>? Lessons { get; set; }
	}
}
