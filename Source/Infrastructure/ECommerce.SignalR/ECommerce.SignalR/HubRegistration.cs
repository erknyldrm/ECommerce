using ECommerce.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace ECommerce.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication  webApplication)
        {
            webApplication.MapHub<ProductHub>("/product-hub");
        }
    }
}
