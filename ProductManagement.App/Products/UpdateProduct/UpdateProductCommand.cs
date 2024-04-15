using MediatR;

namespace ProductManagement.App.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid ProductId, string Name, string Brand, decimal Price)
        : IRequest<UpdateProductCommandResponse>;
}