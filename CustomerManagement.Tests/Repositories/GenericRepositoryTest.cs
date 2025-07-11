using CustomerManagement.Domain;
using CustomerManagement.Infrastructure.Data;
using CustomerManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Tests.Repositories
{

    [TestFixture]
    public class GenericRepositoryTests
    {
        private CMDbContext _dbContext;
        private GenericRepository<Customer> _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CMDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb") // This requires the Microsoft.EntityFrameworkCore.InMemory package
                .Options;

            _dbContext = new CMDbContext(options);
            _repository = new GenericRepository<Customer>(_dbContext);
        }

        [TearDown]
        public void Cleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task Add_AddsEntity()
        {
            var entity = new Customer { Name = "Test" };

            var result = await _repository.Add(entity);
            await _dbContext.SaveChangesAsync();

            Assert.NotNull(result);
            Assert.AreEqual("Test", result.Name);

            var count = _dbContext.Set<Customer>().Count();
            Assert.AreEqual(1, count);
        }

        [Test]
        public void Delete_SetsIsDeleted_True()
        {
            var entity = new Customer();

            var result = _repository.Delete(entity);

            Assert.IsTrue(result);
            Assert.IsTrue(entity.IsDeleted);
        }

        [Test]
        public void Delete_ReturnsFalse_WhenIsDeletedPropertyMissing()
        {
            var noDeleteEntity = new { Id = 1, Name = "Test" }; // anonymous type with no IsDeleted
            var repo = new GenericRepository<object>(_dbContext);

            var result = repo.Delete(noDeleteEntity);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetById_ReturnsCorrectEntity()
        {
            long customerId = 1;
            var entity = new Customer { CustomerId = customerId, Name = "Test" };
            _dbContext.Set<Customer>().Add(entity);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.Get(customerId);

            Assert.NotNull(result);
            Assert.AreEqual("Test", result?.Name);
        }

        [Test]
        public async Task GetAll_ReturnsAllEntities()
        {
            _dbContext.Set<Customer>().AddRange(
                new Customer { Name = "A" },
                new Customer { Name = "B" });
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetAll();

            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void HardDelete_RemovesEntity()
        {
            var entity = new Customer { Name = "ToRemove" };
            _dbContext.Set<Customer>().Add(entity);
            _dbContext.SaveChanges();

            _repository.HardDelete(entity);
            _dbContext.SaveChanges();

            var exists = _dbContext.Set<Customer>().Any(e => e.Name == "ToRemove");
            Assert.IsFalse(exists);
        }

        [Test]
        public void Update_ChangesEntityState()
        {
            var entity = new Customer { Name = "ToUpdate" };
            _dbContext.Set<Customer>().Add(entity);
            _dbContext.SaveChanges();

            var result = _repository.Update(entity);

            Assert.IsTrue(result);
            Assert.AreEqual(EntityState.Modified, _dbContext.Entry(entity).State);
        }
    }
}