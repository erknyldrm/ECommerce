using ECommerce.Application.Abstractions.Storage;
using ECommerce.Application.Repositories;
using MediatR;

namespace ECommerce.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        private readonly IStorageService _storageService;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public UploadProductImageCommandHandler(IStorageService storageService, 
            IProductReadRepository productReadRepository, 
            IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var datas = await _storageService.UploadAsync("files", request.Files);

            var product = await _productReadRepository.GetByIdAsync(request.Id, true);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(p => new Domain.Entities.ProductImageFile()
            {
                FileName = p.fileName,
                Path = p.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
