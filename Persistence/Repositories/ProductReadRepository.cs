using CQRS.ProductManagement.Application.Features.Products.DTOs;
using CQRS.ProductManagement.Application.Interfaces;
using CQRS.ProductManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CQRS.ProductManagement.Persistence.Repositories
{
    public sealed class ProductReadRepository : IProductReadRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductReadRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbContext.ProductReads
                .OrderBy(product => product.Name)
                .Select(product => new ProductDto(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.StockQuantity,
                    product.CreatedAt,
                    product.UpdatedAt))
                .ToListAsync(cancellationToken);
        }

        public Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return dbContext.ProductReads
                .AsNoTracking()
                .Where(product => product.Id == id)
                .Select(product => new ProductDto(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.StockQuantity,
                    product.CreatedAt,
                    product.UpdatedAt))
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
