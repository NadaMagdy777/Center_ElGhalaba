using Center_ElGhalaba.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Center_ElGhlaba.ViewModels
{
    public class AdminLevelVM
    {
        [ValidateNever]
        public List<Stage> Stages { get; set; }

        [Display(Name ="Stage")]
        [Required]
        public int StageID { get; set; }
        
        [Required, MinLength(5), MaxLength(60)]       //=======================> UNIQUE
        public string Name { get; set; }
    }
}
