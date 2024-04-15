using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.App.Exceptions;
using ProductManagement.Domain;

namespace ProductManagement.App.Products.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    ILogger<UpdateProductCommandHandler> logger)
    : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }

            product.Name = request.Name;
            product.Brand = request.Brand;
            product.Price = request.Price;

            await productRepository.UpdateAsync(product, cancellationToken);

            logger.LogInformation("product updated successfully: {productId}", request.ProductId);

            return new UpdateProductCommandResponse(product.Id, product.Name, product.Brand, product.Price);
        }
        catch (Exception e)
        {
            logger.LogError("Failed to update product");
            throw;
        }
    }
}
    