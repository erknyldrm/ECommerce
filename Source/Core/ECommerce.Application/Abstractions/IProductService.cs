using ECommerce.Domain.Entities;

namespace ECommerce.Application.Abstractions
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
