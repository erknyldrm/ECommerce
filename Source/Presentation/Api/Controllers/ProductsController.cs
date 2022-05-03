using ECommerce.Application.Repositories;
using ECommerce.Application.RequestParameters;
using ECommerce.Application.ViewModels.Products;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task GetAll()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new() { Id = Guid.NewGuid(), Name = "Product1", Price = 100, CreateDate = DateTime.UtcNow, Stock = 10 },
                new() { Id = Guid.NewGuid(), Name = "Product2", Price = 80, CreateDate = DateTime.UtcNow, Stock = 7 },
            });

            await _productWriteRepository.SaveAsync();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            List<Product> items = new()
            {
                new() { Id = Guid.NewGuid(), Name = "Product1", Price = 100, CreateDate = DateTime.UtcNow, UpdatedDate = DateTime.Now, Stock = 10 },
                new() { Id = Guid.NewGuid(), Name = "Product2", Price = 80, CreateDate = DateTime.UtcNow, UpdatedDate = DateTime.Now, Stock = 7 },
                new() { Id = Guid.NewGuid(), Name = "Product3", Price = 150, CreateDate = DateTime.UtcNow, UpdatedDate = DateTime.Now, Stock = 2 },
            };
            // var entities = _productReadRepository.GetAll(false);
            // var entitiesCount = _productReadRepository.GetAll(false);
            var entities = items;


            var totalCount = entities.Count();  
            var products = entities.Select(x => new
            {
                x.Id,
                x.Name,
                x.Stock,
                x.Price,
                x.CreateDate,
                x.UpdatedDate
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

            return Ok(new
            {
                products,
                totalCount
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VMCreateProduct model)
        {
            await _productWriteRepository.AddAsync(new Product {  Name = model.Name, Price = model.Price, Stock = model.Stock });
            await _productWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VMUpdateProduct model)
        {
            var product = await _productReadRepository.GetByIdAsync(model.Id);

            product.Stock = model.Stock;
            product.Name = model.Name;  
            product.Price = model.Price;

            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();

            return Ok();
        }
    }
}
