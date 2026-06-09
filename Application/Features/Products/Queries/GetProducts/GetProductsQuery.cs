using CQRS.ProductManagement.Application.Features.Products.DTOs;
using MediatR;

namespace CQRS.ProductManagement.Application.Features.Products.Queries.GetProducts
{
    public sealed record GetProductsQuery : IRequest<IReadOnlyList<ProductDto>>;
}
