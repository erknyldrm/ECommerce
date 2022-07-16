using MediatR;

namespace ECommerce.Application.Features.Queries.GetProductImageFile
{
    public class GetProductImageFileQueryRequest : IRequest<List<GetProductImageFileQueryResponse>>
    {
        public string Id { get; set; }
    }
}
