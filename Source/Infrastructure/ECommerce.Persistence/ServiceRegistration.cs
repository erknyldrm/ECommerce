using Microsoft.EntityFrameworkCore;
using ECommerce.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Application.Repositories;
using ECommerce.Persistence.Repositories;

namespace ECommerce.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ECommerceDbContext>(options =>
                options.UseNpgsql(Configuration.ConnectionString), ServiceLifetime.Singleton);

            serviceCollection.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
            serviceCollection.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();
            serviceCollection.AddSingleton<IOrderReadRepository, OrderReadRepository>();
            serviceCollection.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();
            serviceCollection.AddSingleton<IProductReadRepository, ProductReadRepository>();
            serviceCollection.AddSingleton<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
