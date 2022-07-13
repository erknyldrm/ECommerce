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

            serviceCollection.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            serviceCollection.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            serviceCollection.AddScoped<IOrderReadRepository, OrderReadRepository>();
            serviceCollection.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            serviceCollection.AddScoped<IProductReadRepository, ProductReadRepository>();
            serviceCollection.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            serviceCollection.AddScoped<IFileReadRepository, FileReadRepository>();
            serviceCollection.AddScoped<IFileWriteRepository, FileWriteRepository>();
            serviceCollection.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            serviceCollection.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            serviceCollection.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            serviceCollection.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
        }
    }
}
