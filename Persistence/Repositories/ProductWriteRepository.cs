using CQRS.ProductManagement.Application.Interfaces;
using CQRS.ProductManagement.Domain.Entities;
using CQRS.ProductManagement.Persistence.Context;
using CQRS.ProductManagement.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace CQRS.ProductManagement.Persistence.Repositories
{
    public sealed class ProductWriteRepository : IProductWriteRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductWriteRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
        {
            await dbContext.Products.AddAsync(product, cancellationToken);
            await dbContext.ProductReads.AddAsync(ToReadModel(product), cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return product;
        }

        public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return dbContext.Products.SingleOrDefaultAsync(product => product.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            var readModel = await dbContext.ProductReads
                .SingleOrDefaultAsync(item => item.Id == product.Id, cancellationToken);

            if (readModel is null)
            {
                await dbContext.ProductReads.AddAsync(ToReadModel(product), cancellationToken);
            }
            else
            {
                ApplyReadModelValues(readModel, product);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
        {
            var readModel = await dbContext.ProductReads
                .SingleOrDefaultAsync(item => item.Id == product.Id, cancellationToken);

            if (readModel is not null)
            {
                dbContext.ProductReads.Remove(readModel);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        private static ProductReadModel ToReadModel(Product product)
        {
            return new ProductReadModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
            };
        }

        private static void ApplyReadModelValues(ProductReadModel readModel, Product product)
        {
            readModel.Name = product.Name;
            readModel.Description = product.Description;
            readModel.Price = product.Price;
            readModel.StockQuantity = product.StockQuantity;
            readModel.CreatedAt = product.CreatedAt;
            readModel.UpdatedAt = product.UpdatedAt;
        }
    }
}
