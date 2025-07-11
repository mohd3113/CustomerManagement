using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Domain;
using CustomerManagement.Infrastructure.Data;

namespace CustomerManagement.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CMDbContext dbContext) : base(dbContext)
        {
        }
    }
}