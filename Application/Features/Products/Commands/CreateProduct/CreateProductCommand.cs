using MediatR;

namespace CQRS.ProductManagement.Application.Features.Products.Commands.CreateProduct
{

    public record CreateProductCommand(
        string Name,
        string Description,
        decimal Price,
        int StockQuantity) : IRequest<Guid>;
}
