using ECommerce.Application.Repositories;
using MediatR;

namespace ECommerce.Application.Features.Queries.Product.GetAllProduct
{
    internal class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {

            var totalCount = _productReadRepository.GetAll(false).Count();;
            var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size)
                .Take(request.Size).Select(p => new 
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreateDate,
                    p.UpdatedDate
                }).ToList();

            return new()
            {
                TotalCount = totalCount,
                Products = products
            };
        }
    }
}
