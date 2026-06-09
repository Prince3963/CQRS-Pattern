using CQRS.ProductManagement.Application.Features.Products.DTOs;

namespace CQRS.ProductManagement.Application.Interfaces
{
    public interface IProductReadRepository
    {
        Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
