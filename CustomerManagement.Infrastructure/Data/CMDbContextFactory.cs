using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CustomerManagement.Infrastructure.Data
{
    public class CMDbContextFactory : IDesignTimeDbContextFactory<CMDbContext>
    {
        CMDbContext IDesignTimeDbContextFactory<CMDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CMDbContext>();
            optionsBuilder.UseSqlServer("");

            return new CMDbContext(optionsBuilder.Options);
        }
    }
}