using CQRS.ProductManagement.Application.Features.Products.DTOs;
using MediatR;

namespace CQRS.ProductManagement.Application.Features.Products.Queries.GetProductById
{
    public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
}
