using Center_ElGhalaba.Models;
using Center_ElGhlaba.Constants;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Center_ElGhlaba.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Student(StudentPaymentVM vm)
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
            Lesson lesson = await unit.Lessons.GetByIdAsync(vm.LessonID);
            Teacher teacher = await unit.Teachers.GetByIdAsync(lesson.TeacherID);
            StudentOrder order = new StudentOrder();
            order.LessonID = vm.LessonID;
            order.StudentID = unit.Students.FindAsync(s => s.AppUserID == vm.AppUserID).Result.ID;
            order.Date = DateTime.Now;
            order.Price = vm.Price;
            order.Discount = vm.Discount;
            order.PaymentName = vm.PaymentName;
            order.PaymentValue = vm.PaymentValue;

            teacher.Balance += vm.Net;

            
            unit.Orders.Insert(order);
            unit.Complete();

            return RedirectToAction("PaymentSuccess", vm);
        }

        public async Task<IActionResult> Teacher(int Id)
        {
            Teacher teacher = await unit.Teachers.FindAsync(t => t.ID == Id, new string[] { "AppUser" });
            if (teacher == null)
            {
                return RedirectToAction("Index", "Home");
            }

            TeacherPaymentVM vm = new()
            {
                Name = $"Mr. {teacher.AppUser.FirstName} {teacher.AppUser.LastName}",
                ID = teacher.ID,
                Balance = teacher.Balance,
                PaymentOptions = new(),
            };

            foreach (PaymentOptions opt in Enum.GetValues(typeof(PaymentOptions)))
            {
                string option = opt.ToString().Replace("_", " ");
                vm.PaymentOptions.Add(option);
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Teacher(TeacherPaymentVM vm)
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

            Teacher teacher = await unit.Teachers.FindAsync(t => t.ID == vm.ID, new string[] { "AppUser" });

            if (teacher.Balance < vm.WithdrawlAmount)
            {
                ModelState.AddModelError("WithdrawlAmount", "Insufficient balance");
                vm.PaymentOptions = new();
                foreach (PaymentOptions opt in Enum.GetValues(typeof(PaymentOptions)))
                {
                    string option = opt.ToString().Replace("_", " ");
                    vm.PaymentOptions.Add(option);
                }
                return View(vm);
            }

            TeacherPaymentMethod payment = new()
            {
                TeacherID = vm.ID,
                PaymentName = vm.PaymentName,
                PaymentVlaue = vm.PaymentValue,
            };

            TeacherLogs log = new()
            {
                Amount = vm.WithdrawlAmount,
                Date = DateTime.Now,
                TeacherID = vm.ID,
                TeacherPaymentMethod = payment,
            };

            teacher.Balance -= vm.WithdrawlAmount;

            unit.Teachers.Update(teacher);
            unit.TeacherLogs.Insert(log);
            unit.Complete();

            return RedirectToAction("Details", "Teachers", new { id = teacher.AppUserID }); 
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
