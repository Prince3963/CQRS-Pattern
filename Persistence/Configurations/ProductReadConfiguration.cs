using CQRS.ProductManagement.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.ProductManagement.Persistence.Configurations
{
    public sealed class ProductReadConfiguration : IEntityTypeConfiguration<ProductReadModel>
    {
        public void Configure(EntityTypeBuilder<ProductReadModel> builder)
        {
            builder.ToTable("Products", "read");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.StockQuantity)
                .IsRequired();
        }
    }
}
