namespace CustomerManagement.Application.Contracts.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> Get(int id);

        Task<T?> Get(long id);

        Task<List<T>> GetAll();

        Task<T> Add(T entity);

        bool Update(T entity);

        bool Delete(T entity);

        void HardDelete(T entity);
    }
}