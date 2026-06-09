using CQRS.ProductManagement.Application.Features.Products.Commands.CreateProduct;
using CQRS.ProductManagement.Application.Features.Products.Commands.DeleteProduct;
using CQRS.ProductManagement.Application.Features.Products.Commands.UpdateProduct;
using CQRS.ProductManagement.Application.Features.Products.Queries.GetProductById;
using CQRS.ProductManagement.Application.Features.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.ProductManagement.API.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var products = await mediator.Send(new GetProductsQuery(), cancellationToken);
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            var productId = await mediator.Send(command, cancellationToken);
            //return CreatedAtAction(nameof(GetProductById), new { id = productId }, new { id = productId });
            return Ok(productId);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(
            Guid id,
            [FromBody] UpdateProductRequest request,
            CancellationToken cancellationToken)
        {
            await mediator.Send(
                new UpdateProductCommand(
                    id,
                    request.Name,
                    request.Description,
                    request.Price,
                    request.StockQuantity),
                cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteProductCommand(id), cancellationToken);
            return NoContent();
        }
    }

    public sealed record UpdateProductRequest(
        string Name,
        string Description,
        decimal Price,
        int StockQuantity);
}
