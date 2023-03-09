using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Center_ElGhalaba.Models
{
    public class Stage
	{
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual List<Level>? Levels { get; set; }
    }
}
