using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Domain;
using CustomerManagement.Infrastructure.Data;

namespace CustomerManagement.Infrastructure.Repositories
{
    public class CommunicationTemplateRepository : GenericRepository<CommunicationTemplate>, ICommunicationTemplateRepository
    {
        public CommunicationTemplateRepository(CMDbContext dbContext) : base(dbContext)
        {
        }
    }
}
