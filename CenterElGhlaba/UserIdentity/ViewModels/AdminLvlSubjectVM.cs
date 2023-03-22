using Center_ElGhalaba.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Center_ElGhlaba.ViewModels
{
    public class AdminLvlSubjectVM
    {
        [ValidateNever]
        public List<Stage> Stages { get; set; }
        
        [Required]
        [Display(Name = "Stage")]
        public int StageID { get; set; }
        
        [ValidateNever]
        public List<Level> Levels { get; set; }
        
        [Required]
        [Display(Name = "Level")]
        public int LevelID { get; set; }

        [Required, MinLength(5), MaxLength(60)]       //=======================> UNIQUE
        public string Name { get; set; }
    }
}
