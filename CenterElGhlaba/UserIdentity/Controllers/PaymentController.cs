using Center_ElGhalaba.Models;
using Center_ElGhlaba.Constants;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Center_ElGhlaba.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork unit;

        public PaymentController(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<IActionResult> Student(string AppUserID, int ID)
        {
            Lesson lesson = await unit.Lessons.FindAsync(l => l.ID == ID);
            Student student = await unit.Students.FindAsync(s => s.AppUserID == AppUserID, new string[] { "AppUser" });

            if (lesson == null || student == null || student.AppUser == null)
            {
                return Redirect("Lesson/Index");
            }

            StudentPaymentVM vm = new()
            {
                AppUserID = AppUserID,
                LessonID = ID,
                LessonTitle = lesson.Title,
                //LessonCoverPicture = lesson.CoverPicture,
                Price = lesson.Price,
                Discount = lesson.Discount,
                Net = lesson.Price - lesson.Discount,
                StudentName = $"{student.AppUser.FirstName} {student.AppUser.LastName}",
                PaymentOptions = new()
            };

            foreach (PaymentOptions opt in Enum.GetValues(typeof(PaymentOptions)))
            {
                string option = opt.ToString().Replace("_", " ");
                vm.PaymentOptions.Add(option);
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult Student(StudentPaymentVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PaymentOptions = new();
                foreach (PaymentOptions opt in Enum.GetValues(typeof(PaymentOptions)))
                {
                    string option = opt.ToString().Replace("_", " ");
                    vm.PaymentOptions.Add(option);
                }
                return View(vm);
            }

            StudentOrder order = new()
            {
                LessonID = vm.LessonID,
                StudentID = unit.Students.FindAsync(s => s.AppUserID == vm.AppUserID).Result.ID,
                Date = DateTime.Now,
                Price = vm.Price,
                Discount = vm.Discount,
                PaymentName = vm.PaymentName,
                PaymentValue = vm.PaymentValue,
            };

            unit.Orders.Insert(order);
            unit.Complete();

            return RedirectToAction("PaymentSuccess", vm);
        }

        public async Task<IActionResult> PaymentSuccess(StudentPaymentVM vm)
        {
            Lesson lesson = await unit.Lessons.FindAsync(l => l.ID == vm.LessonID);
            Student student = await unit.Students.FindAsync(s => s.AppUserID == vm.AppUserID, new string[] { "AppUser" });

            if (lesson == null || student == null || student.AppUser == null)
            {
                return Redirect("Lesson/Index");
            }

            vm.StudentName = $"{student.AppUser.FirstName} {student.AppUser.LastName}";
            vm.LessonTitle = lesson.Title;
            vm.LessonCoverPicture = lesson.CoverPicture;

            return View(vm);
        }
    }
}
