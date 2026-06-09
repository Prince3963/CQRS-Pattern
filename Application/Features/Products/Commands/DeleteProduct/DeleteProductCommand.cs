using MediatR;

namespace CQRS.ProductManagement.Application.Features.Products.Commands.DeleteProduct
{
    public sealed record DeleteProductCommand(Guid Id) : IRequest;
}
