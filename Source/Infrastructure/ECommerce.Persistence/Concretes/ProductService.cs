using ECommerce.Application.Abstractions;
using ECommerce.Domain.Entities;

namespace ECommerce.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
            => new()
            {
                new() { Id = Guid.NewGuid(), Name = "Product1", Price = 100, Stock = 10 },
                new() { Id = Guid.NewGuid(), Name = "Product2", Price = 150, Stock = 5 },
                new() { Id = Guid.NewGuid(), Name = "Product3", Price = 50, Stock = 80 },
                new() { Id = Guid.NewGuid(), Name = "Product4", Price = 120, Stock = 10 },
                new() { Id = Guid.NewGuid(), Name = "Product5", Price = 150, Stock = 10 },
            };
    }
}
