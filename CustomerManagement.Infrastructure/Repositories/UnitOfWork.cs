using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Http;

namespace CustomerManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CMDbContext _dbContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private bool isDisposed = false;

        public UnitOfWork(CMDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        private ICommunicationTemplateRepository _communicationTemplateRepository;
        public ICommunicationTemplateRepository CommunicationTemplateRepository
        {
            get
            {
                if (_communicationTemplateRepository == null)
                {
                    _communicationTemplateRepository = new CommunicationTemplateRepository(_dbContext);
                }
                return _communicationTemplateRepository;
            }
        }

        private ICustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(_dbContext);
                }
                return _customerRepository;
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            isDisposed = true;
        }
        public async Task SaveChanges()
        {
            var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
            await _dbContext.SaveChangesAsync(username);
        }
    }
}