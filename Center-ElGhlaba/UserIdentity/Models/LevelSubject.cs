using System.ComponentModel.DataAnnotations.Schema;

namespace Center_ElGhalaba.Models
{
    public class LevelSubject
    {
        public int ID { get; set; }

        [ForeignKey("Level")]
        public int LevelID { get; set; }
        public virtual Level? Level { get; set; }

        [ForeignKey("Stage")]
        public int StageID { get; set; }
        public virtual Stage? Stage { get; set; }
    }
}
