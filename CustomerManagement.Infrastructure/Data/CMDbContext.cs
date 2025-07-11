using CustomerManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Data
{
    public class CMDbContext : AuditableDbContext
    {
        public CMDbContext(DbContextOptions<CMDbContext> options) : base(options) { }

        public DbSet<CommunicationTemplate> CommunicationTemplates { get; set; }

        public DbSet<Customer> Customers { get; set; }
    }
}