using ECommerce.Application.Repositories;
using MediatR;

namespace ECommerce.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new Domain.Entities.Product
            { 
                Name = request.Name, 
                Price = request.Price, 
                Stock = request.Stock 
            });
            await _productWriteRepository.SaveAsync();

            return new();
        }
    }
}
