using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Center_ElGhlaba.ViewModels
{
    public class StudentPaymentVM
    {
        public string AppUserID { get; set; }
        public int LessonID { get; set; }

        [ValidateNever]
        public string LessonTitle { get; set; }
        
        [ValidateNever]
        public byte[] LessonCoverPicture { get; set; }
        
        [ValidateNever]
        public string StudentName { get; set; }

        [Required]
        public string PaymentName { get; set; }
        
        [Required]
        [MaxLength(16)]
        [MinLength(11)]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public string PaymentValue { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Net { get; set; }
        
        [ValidateNever]
        public List<string> PaymentOptions { get; set; }
    }
}
