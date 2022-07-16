using ECommerce.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Application.Features.Queries.GetProductImageFile
{
    public class GetProductImageFileQueryHandler : IRequestHandler<GetProductImageFileQueryRequest, List<GetProductImageFileQueryResponse>>
    {
        private readonly IConfiguration _configuration;
        private readonly IProductReadRepository _productReadRepository;

        public GetProductImageFileQueryHandler(IConfiguration configuration, IProductReadRepository productReadRepository)
        {
            _configuration = configuration;
            _productReadRepository = productReadRepository;
        }

        public async Task<List<GetProductImageFileQueryResponse>> Handle(GetProductImageFileQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
               .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));

            return product?.ProductImageFiles.Select(p => new GetProductImageFileQueryResponse
            {
                Id = p.Id,
                FileName =  p.FileName,
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}"
            }).ToList();
        }
    }
}
