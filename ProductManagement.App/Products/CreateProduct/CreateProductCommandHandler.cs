using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Domain;

namespace ProductManagement.App.Products.CreateProduct;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    ILogger<CreateProductCommandHandler> logger)
    : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Brand = request.Brand,
                Price = request.Price
            };

            await productRepository.AddAsync(product, cancellationToken);

            logger.LogInformation("product created successfully: {productId}", product.Id);

            return new CreateProductCommandResponse(product.Id);
        }
        catch (Exception exception)
        {
            logger.LogError("Failed to create product: " + exception.Message);
            throw;
        }
    }
}
    