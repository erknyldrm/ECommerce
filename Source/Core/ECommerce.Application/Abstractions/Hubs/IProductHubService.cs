using ECommerce.Domain.Entities;

namespace ECommerce.Application.Abstractions.Hubs
{
    public interface IProductHubService
    {
        Task ProductAddedMessageAsync(string message);
    }
}
