using CQRS.ProductManagement.Application.Interfaces;
using CQRS.ProductManagement.Domain.Entities;
using MediatR;

namespace CQRS.ProductManagement.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductWriteRepository productWriteRepository;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            this.productWriteRepository = productWriteRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                StockQuantity = request.StockQuantity,
            };

            await productWriteRepository.AddAsync(product, cancellationToken);

            return product.Id;
        }
    }
}
