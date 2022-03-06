using ECommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerce.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceDbContext>
    {
        public ECommerceDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ECommerceDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new ECommerceDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
