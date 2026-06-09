using CQRS.ProductManagement.Application.Common.Exceptions;
using CQRS.ProductManagement.Application.Interfaces;
using MediatR;

namespace CQRS.ProductManagement.Application.Features.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductWriteRepository productWriteRepository;

        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            this.productWriteRepository = productWriteRepository;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productWriteRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException(nameof(product), request.Id);
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            product.UpdatedAt = DateTime.UtcNow;

            await productWriteRepository.UpdateAsync(product, cancellationToken);
        }
    }
}
