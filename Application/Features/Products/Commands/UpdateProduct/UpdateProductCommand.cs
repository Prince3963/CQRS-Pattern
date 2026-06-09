using MediatR;

namespace CQRS.ProductManagement.Application.Features.Products.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        int StockQuantity) : IRequest;
}
