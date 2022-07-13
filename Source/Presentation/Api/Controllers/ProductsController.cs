using ECommerce.Application.Abstractions.Storage;
using ECommerce.Application.Repositories;
using ECommerce.Application.RequestParameters;
using ECommerce.Application.ViewModels.Products;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;

        public ProductsController(IProductReadRepository productReadRepository, 
                                  IProductWriteRepository productWriteRepository,
                                  IFileWriteRepository fileWriteRepository,
                                  IFileReadRepository fileReadRepository,
                                  IProductImageFileWriteRepository productImageFileWriteRepository,
                                  IProductImageFileReadRepository productImageFileReadRepository,
                                  IInvoiceFileWriteRepository invoiceFileWriteRepository,
                                  IInvoiceFileReadRepository invoiceFileReadRepository,
                                  IStorageService storageService,
                                  IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _storageService = storageService;
            _configuration = configuration;
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

        [HttpGet("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            //var datas = await _storageService.UploadAsync("resources/files", Request.Form.Files);
            var datas = await _storageService.UploadAsync("files", Request.Form.Files); // for Azure,AWS 

            var product = await _productReadRepository.GetByIdAsync(id, true);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(p => new ProductImageFile()
            {
                FileName = p.fileName,
                Path = p.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            return Ok(product.ProductImageFiles.Select(p => new 
            {
                p.Id,
                p.FileName,
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}" 
            }));

        }

        [HttpDelete("[action/{productId}/{Id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            var productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));

            product.ProductImageFiles.Remove(productImageFile);

            await _productImageFileWriteRepository.SaveAsync();

            return Ok();

        }

    }
}
