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

namespace Cener_Test
{
    [TestFixture]
    public class LevelTest
    {
        private ApplicationDbContext _dbContext;
        private BaseRepository<Level> _levelRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            // use an in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _levelRepository = new BaseRepository<Level>(_dbContext);

            // add some test data
            var levels = new List<Level>
            {
                new Level { Name = "Beginner" },
                new Level { Name = "Intermediate" },
                new Level { Name = "Advanced" }
            };

            _dbContext.AddRange(levels);
            _dbContext.SaveChanges();
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllLevels()
        {
            // act
            var result = await _levelRepository.GetAllAsync();

            // assert
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsCorrectLevel()
        {
            // arrange
            var expectedLevel = await _dbContext.Levels.FirstOrDefaultAsync(l => l.Name == "Intermediate");

            // act
            var result = await _levelRepository.GetByIdAsync(expectedLevel.ID);

            // assert
            Assert.AreEqual(expectedLevel.ID, result.ID);
            Assert.AreEqual(expectedLevel.Name, result.Name);
        }

    }
}
