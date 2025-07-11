using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly CMDbContext _dbContext;

        public GenericRepository(CMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public bool Delete(T entity)
        {
            var prop = entity.GetType().GetProperty("IsDeleted");
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(entity, true);
                return true;
            }
            return false;
        }

        public async Task<T?> Get(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> Get(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public void HardDelete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public bool Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}
