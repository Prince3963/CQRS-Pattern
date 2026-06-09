using CQRS.ProductManagement.Application.Common.Exceptions;
using CQRS.ProductManagement.Application.Features.Products.DTOs;
using CQRS.ProductManagement.Application.Interfaces;
using MediatR;

namespace CQRS.ProductManagement.Application.Features.Products.Queries.GetProductById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductReadRepository productReadRepository;

        public GetProductByIdQueryHandler(IProductReadRepository productReadRepository)
        {
            this.productReadRepository = productReadRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productReadRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException(nameof(product), request.Id);
            }

            return product;
        }
    }
}
