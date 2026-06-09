using CQRS.ProductManagement.Domain.Entities;
using CQRS.ProductManagement.Persistence.Context;
using CQRS.ProductManagement.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace CQRS.ProductManagement.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    public partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.Entity<Product>(builder =>
            {
                builder.Property<Guid>(product => product.Id)
                    .HasColumnType("uniqueidentifier");

                builder.Property<DateTime>(product => product.CreatedAt)
                    .HasColumnType("datetime2");

                builder.Property<string>(product => product.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnType("nvarchar(1000)");

                builder.Property<bool>(product => product.IsDeleted)
                    .HasColumnType("bit")
                    .HasDefaultValue(false);

                builder.Property<string>(product => product.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                builder.Property<decimal>(product => product.Price)
                    .HasColumnType("decimal(18,2)");

                builder.Property<int>(product => product.StockQuantity)
                    .HasColumnType("int");

                builder.Property<DateTime?>(product => product.UpdatedAt)
                    .HasColumnType("datetime2");

                builder.HasKey(product => product.Id);

                builder.ToTable("Products", "write");

                builder.HasQueryFilter(product => !product.IsDeleted);
            });

            modelBuilder.Entity<ProductReadModel>(builder =>
            {
                builder.Property<Guid>(product => product.Id)
                    .HasColumnType("uniqueidentifier");

                builder.Property<DateTime>(product => product.CreatedAt)
                    .HasColumnType("datetime2");

                builder.Property<string>(product => product.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnType("nvarchar(1000)");

                builder.Property<string>(product => product.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                builder.Property<decimal>(product => product.Price)
                    .HasColumnType("decimal(18,2)");

                builder.Property<int>(product => product.StockQuantity)
                    .HasColumnType("int");

                builder.Property<DateTime?>(product => product.UpdatedAt)
                    .HasColumnType("datetime2");

                builder.HasKey(product => product.Id);

                builder.ToTable("Products", "read");
            });
        }
    }
}
