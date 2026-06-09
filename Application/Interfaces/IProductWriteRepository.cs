using CQRS.ProductManagement.Domain.Entities;

namespace CQRS.ProductManagement.Application.Interfaces
{
    public interface IProductWriteRepository
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Product product, CancellationToken cancellationToken);
        Task DeleteAsync(Product product, CancellationToken cancellationToken);
    }
}
