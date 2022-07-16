using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
