using ECommerce.Application.Abstractions.Hubs;
using ECommerce.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IProductHubService, ProductHubService>();
            serviceCollection.AddSignalR();
        }
    }
}
