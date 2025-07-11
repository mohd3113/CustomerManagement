using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CustomerManagement.Infrastructure.Data
{
    public class CMDbContextFactory : IDesignTimeDbContextFactory<CMDbContext>
    {
        CMDbContext IDesignTimeDbContextFactory<CMDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CMDbContext>();
            optionsBuilder.UseSqlServer("Data Source=SQL6033.site4now.net;Initial Catalog=db_aa8268_cmdatabase;User Id=db_aa8268_cmdatabase_admin;Password=TestTestcm1");

            return new CMDbContext(optionsBuilder.Options);
        }
    }
}