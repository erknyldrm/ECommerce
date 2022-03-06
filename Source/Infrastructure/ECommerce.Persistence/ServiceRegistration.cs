using Microsoft.EntityFrameworkCore;
using ECommerce.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ECommerceDbContext>(options => 
                options.UseNpgsql(Configuration.ConnectionString));
        }
    }
}
