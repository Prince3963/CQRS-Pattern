using CQRS.ProductManagement.Application.Features.Products.DTOs;
using CQRS.ProductManagement.Application.Interfaces;
using MediatR;

namespace CQRS.ProductManagement.Application.Features.Products.Queries.GetProducts
{
    public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IReadOnlyList<ProductDto>>
    {
        private readonly IProductReadRepository productReadRepository;

        public GetProductsQueryHandler(IProductReadRepository productReadRepository)
        {
            this.productReadRepository = productReadRepository;
        }

        public Task<IReadOnlyList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return productReadRepository.GetAllAsync(cancellationToken);
        }
    }
}
