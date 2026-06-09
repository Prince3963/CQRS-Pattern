using CQRS.ProductManagement.Domain.Entities;
using CQRS.ProductManagement.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace CQRS.ProductManagement.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReadModel> ProductReads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ApplyConfigurationsFromAssembly - automatically loads all entity configuration files.
            #endregion

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }
    }
}
