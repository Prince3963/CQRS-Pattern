
namespace CQRS.ProductManagement.Application.Features.Products.DTOs
{
    public sealed record ProductDto(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        int StockQuantity,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}
