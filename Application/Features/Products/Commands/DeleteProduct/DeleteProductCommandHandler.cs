using CQRS.ProductManagement.Application.Common.Exceptions;
using CQRS.ProductManagement.Application.Interfaces;
using MediatR;

namespace CQRS.ProductManagement.Application.Features.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductWriteRepository productWriteRepository;

        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            this.productWriteRepository = productWriteRepository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productWriteRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException(nameof(product), request.Id);
            }

            product.IsDeleted = true;
            product.UpdatedAt = DateTime.UtcNow;

            await productWriteRepository.DeleteAsync(product, cancellationToken);
        }
    }
}
