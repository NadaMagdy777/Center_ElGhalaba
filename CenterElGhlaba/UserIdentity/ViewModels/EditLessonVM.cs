using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Center_ElGhlaba.ViewModels
{
    public class EditLessonVM
    {
        public int ID { get; set; }

        [MinLength(4, ErrorMessage = "Name Must be more than 3 letter")]
        [MaxLength(20, ErrorMessage = "Name Must be less than 10 letter")]
        public string Title { get; set; }

        [MinLength(10, ErrorMessage = "Description Must be more than 10 letter")]
        [MaxLength(50, ErrorMessage = "Description must be less than 50 letter")]
        public string Description { get; set; }

        [Range(minimum: 20, maximum: 100,ErrorMessage ="Price Must be between 20 and 100")]
        public decimal Price { get; set; }

        [Remote("CheckDiscount", "Lesson", AdditionalFields = "Price",
           ErrorMessage = "Discount Should be Less Than Lesson Price")]
        public decimal Discount { get; set; }

    }
}
