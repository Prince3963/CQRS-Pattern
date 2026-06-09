using CQRS.ProductManagement.Domain.Common;

namespace CQRS.ProductManagement.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
