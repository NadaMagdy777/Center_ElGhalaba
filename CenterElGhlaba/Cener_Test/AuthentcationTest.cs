using Center_ElGhalaba.Models;
using Center_ElGhlaba.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Data;
using UserIdentity.Models;

namespace Cener_Test
{
    [TestFixture]
    public class AuthentcationTest
    {
        private ApplicationDbContext _dbContext;
        private BaseRepository<ApplicationUser> _AdminRepository;
        private BaseRepository<Teacher> _TeacherRepository;
        private BaseRepository<Student> _StudentRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            // use an in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _AdminRepository = new BaseRepository<ApplicationUser>(_dbContext);
            _TeacherRepository = new BaseRepository<Teacher>(_dbContext);
            _StudentRepository = new BaseRepository<Student>(_dbContext);

            var Admin = new ApplicationUser
            {
                FirstName = "Moeen",
                LastName = "Adly",
                Address = "London",
                Birthdate = new DateTime(1998, 4, 1),
                JoinDate = DateTime.Now,
                IsAvailable = true,
                IsDeleted = false,
                PhoneNumber = "0110101044",
                PasswordHash = "AQAAAAIAAYagAAAAEMZm7dfE0pRMkAd3D4UgqdNScV/TcNJ8lUGcE8jmDfpGgWmS6YN0UnVhZEAuSkfHQA==",
                Email = "mm@gmail.com",
                EmailConfirmed = true,
            };

            var Teacher = new Teacher
            {
                AppUser = new ApplicationUser 
                {
                    FirstName = "Abdullah",
                    LastName = "Yaser",
                    Address = "China",
                    Birthdate = new DateTime(1998, 5, 15),
                    JoinDate = DateTime.Now,
                    IsAvailable = true,
                    IsDeleted = false,
                    PhoneNumber = "01010101077",
                    PasswordHash = "AQAAAAIAAYagAAAAEMZm7dfE0pRMkAd3D4UgqdNScV/TcNJ8lUGcE8jmDfpGgWmS6YN0UnVhZEAuSkfHQA==",
                    Email = "aa@gmail.com",
                    EmailConfirmed = true,
                },
                Balance = 0
            };
            var Student = new Student
            {
                AppUser = new ApplicationUser
                {
                    FirstName = "Shaima",
                    LastName = "Ahmed",
                    Address = "London",
                    Birthdate = new DateTime(1999, 3, 17),
                    JoinDate = DateTime.Now,
                    IsAvailable = true,
                    IsDeleted = false,
                    PhoneNumber = "01510101077",
                    PasswordHash = "AQAAAAIAAYagAAAAEMZm7dfE0pRMkAd3D4UgqdNScV/TcNJ8lUGcE8jmDfpGgWmS6YN0UnVhZEAuSkfHQA==",
                    Email = "ss@gmail.com",
                    EmailConfirmed = true,
                }               
            };
            _dbContext.Add(Admin);
            _dbContext.Add(Student);
            _dbContext.Add(Teacher);

            _dbContext.SaveChanges();
        }

        [Test]
        public async Task GetAllAdminsAsync()
        {
            // act
            var result = await _AdminRepository.GetAllAsync();

            // assert
            Assert.AreEqual(3, result.Count);
        }
        [Test]
        public async Task GetAllTeachersAsync()
        {
            // act
            var result = await _TeacherRepository.GetAllAsync();

            // assert
            Assert.AreEqual(1, result.Count);
        }
        [Test]
        public async Task GetAllStudentsAsync()
        {
            // act
            var result = await _StudentRepository.GetAllAsync();

            // assert
            Assert.AreEqual(1, result.Count);
        }


    }
}
