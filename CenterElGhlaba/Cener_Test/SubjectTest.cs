using Center_ElGhalaba.Constants;
using Center_ElGhalaba.Models;
using Center_ElGhlaba.Interfaces;
using Center_ElGhlaba.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Data;

namespace Cener_Test
{
    [TestFixture]
    public class SubjectTest
    {
        private ApplicationDbContext _dbContext;
        private IBaseRepository<Subject> _subjectRepository;

        [OneTimeSetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _subjectRepository = new BaseRepository<Subject>(_dbContext);

            var subjects = new List<Subject>
            {
                new Subject { ID = 1, Name = "Mathematics", LevelSubjects = new(){
                    new LevelSubject(){
                        Stage = new(){ID = 1, Name = "first Stage" },
                        Level = new() {ID = 1, Name = "first Level" },
                    },
                    new LevelSubject(){
                        Stage = new(){ID = 2, Name = "second Stage" },
                        Level = new() {ID = 2, Name = "second Level" },
                    },
                }},
                new Subject { ID = 2, Name = "Science", LevelSubjects = new(){
                    new LevelSubject(){
                        Stage = new(){ID = 3, Name = "third Stage" },
                        Level = new() {ID = 3, Name = "third Level" },
                    },
                }},

                new Subject { ID = 3, Name = "English", LevelSubjects = new(){
                    new LevelSubject(){
                        Stage = new(){ID = 4, Name = "fourth Stage" },
                        Level = new() {ID = 4, Name = "fourth Level" },
                    }
                }}
            };

            _dbContext.Subjects.AddRange(subjects);
            _dbContext.SaveChanges();
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllSubjects()
        {
            // Act
            var result = await _subjectRepository.GetAllAsync();

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(s => s.Name == "Mathematics"));
            Assert.IsTrue(result.Any(s => s.Name == "Science"));
            Assert.IsTrue(result.Any(s => s.Name == "English"));
        }

        [Test]
        public async Task GetByIdAsync_WithValidId_ShouldReturnSubject()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _subjectRepository.GetByIdAsync(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.ID);
            Assert.AreEqual("Mathematics", result.Name);
        }

        [Test]
        public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            int id = 100;

            // Act
            var result = await _subjectRepository.GetByIdAsync(id);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task FindAllAsync_ShouldReturnMatchingSubjects()
        {
            // Arrange
            Expression<Func<Subject, bool>> criteria = s => s.Name.Contains("ma");

            // Act
            var result = await _subjectRepository.FindAllAsync(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Mathematics", result[0].Name);
        }

        [Test]
        public async Task FindAllAsync_WithIncludes_ShouldReturnSubjectsAndIncludeRelatedData()
        {
            // Arrange
            string[] includes = { "LevelSubjects" };

            // Act
            var result = await _subjectRepository.FindAllAsync(
                s => s.Name == "Mathematics",
                includes: includes
            );

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Mathematics", result[0].Name);
            Assert.AreEqual(2, result[0].LevelSubjects.Count);
        }


        [Test]
        public async Task FindAllAsync_WithCriteria_ReturnsMatchingSubjects()
        {
            // Arrange
            Expression<Func<Subject, bool>> criteria = s => s.Name.Contains("Ma");

            // Act
            var result = await _subjectRepository.FindAllAsync(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.Any(s => s.Name == "Mathematics"));

        }

        [Test]
        public async Task FindAllAsync_WithIncludes_ReturnsSubjectsAndIncludes()
        {
            // Arrange
            string[] includes = { "LevelSubjects" };

            // Act
            var result = await _subjectRepository.FindAllAsync(s => true, includes);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(s => s.LevelSubjects.Any(c => c.SubjectID == 1)));
        }

        [Test]
        public async Task FindAllAsync_WithOrderByDescending_ReturnsSubjectsInDescendingOrder()
        {
            // Arrange
            Expression<Func<Subject, object>> orderBy = s => s.Name;
            string orderByDirection = OrderBy.Descending;

            // Act
            var result = await _subjectRepository.FindAllAsync(
                s => true,
                orderBy: orderBy,
                orderByDirection: orderByDirection
            );

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Science", result[0].Name);
            Assert.AreEqual("Mathematics", result[1].Name);
            Assert.AreEqual("English", result[2].Name);
        }
        [Test]
        public async Task FindAllAsync_WithOrderByAscending_ReturnsSubjectsInAscendigOrder()
        {
            // Arrange
            Expression<Func<Subject, object>> orderBy = s => s.Name.Length;
            string orderByDirection = OrderBy.Ascending;

            // Act
            var result = await _subjectRepository.FindAllAsync(t => true, null, orderBy, orderByDirection);

            // Assert
            Assert.AreEqual("Mathematics", result[2].Name);
            Assert.AreEqual("English", result[1].Name);
            Assert.AreEqual("Science", result[0].Name);
        }

        [Test]
        public async Task FindAllAsync_WithCriteriaAndIncludesAndOrderBy_ReturnsMatchingSubjectsAndIncludesAndOrdered()
        {
            // Arrange
            Expression<Func<Subject, bool>> criteria = s => s.Name.Contains("e");
            string[] includes = { "LevelSubjects" };
            Expression<Func<Subject, object>> orderBy = s => s.Name.Length;

            // Act
            var result = await _subjectRepository.FindAllAsync(criteria, includes, orderBy);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(s => s.LevelSubjects.Any(s => s.LevelID == 1)));
            Assert.AreEqual("Science", result[0].Name);
        }

        [Test]
        public async Task CountAsync_WithValidCriteria_ShouldReturnCount()
        {
            // Arrange
            Expression<Func<Subject, bool>> criteria = s => s.Name.Contains("s");

            // Act
            var result = await _subjectRepository.CountAsync(criteria);

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task CountAsync_WithInvalidCriteria_ShouldReturnZero()
        {
            // Arrange
            Expression<Func<Subject, bool>> criteria = s => s.Name.Contains("z");

            // Act
            var result = await _subjectRepository.CountAsync(criteria);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
