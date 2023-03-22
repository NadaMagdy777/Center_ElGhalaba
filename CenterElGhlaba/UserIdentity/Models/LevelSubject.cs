using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Center_ElGhalaba.Models
{
    public class LevelSubject
    {
        public int ID { get; set; }

        [ForeignKey("Level")]
        public int LevelID { get; set; }
        [JsonIgnore]

        public virtual Level? Level { get; set; }

        [ForeignKey("Stage")]
        public int StageID { get; set; }
        [JsonIgnore]

        public virtual Stage? Stage { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        [JsonIgnore]
        public virtual Subject? Subject { get; set; }
    }
}
