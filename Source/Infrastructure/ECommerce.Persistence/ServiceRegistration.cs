using ECommerce.Application.Abstractions;
using ECommerce.Persistence.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IProductService, ProductService>();
        }
    }
}
