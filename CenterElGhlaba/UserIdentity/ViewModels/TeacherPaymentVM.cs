using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Center_ElGhlaba.ViewModels
{
    public class TeacherPaymentVM
    {
        [Required]
        public int ID { get; set; }
        
        [ValidateNever]
        public string Name { get; set; }

        [ValidateNever]
        public decimal Balance { get; set; }

        [Required]
        public decimal WithdrawlAmount { get; set; }

        [Required]
        public string PaymentName { get; set; }

        [Required]
        [MaxLength(16)]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public string PaymentValue { get; set; }

        [ValidateNever]
        public List<string> PaymentOptions { get; set; }
    }
}
