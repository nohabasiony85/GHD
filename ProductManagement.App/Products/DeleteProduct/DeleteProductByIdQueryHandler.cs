using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Domain;

namespace ProductManagement.App.Products.DeleteProduct;

public class DeleteProductByIdCommandHandler(
    IProductRepository productRepository,
    ILogger<DeleteProductByIdCommandHandler> logger)
    : IRequestHandler<DeleteProductByIdCommand>
{
    public async Task Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await productRepository.DeleteAsync(request.Id, cancellationToken);

            logger.LogInformation("product deleted successfully: {productId}", request.Id);
        }
        catch (Exception exception)
        {
            logger.LogError("Failed to delete product with ID: {productId} " + exception.Message, request.Id);
            throw;
        }
    }
}
