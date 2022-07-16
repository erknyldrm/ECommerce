using ECommerce.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
           Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
           .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));

            var productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));

            product.ProductImageFiles.Remove(productImageFile);

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
