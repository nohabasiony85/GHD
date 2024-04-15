using MediatR;

namespace ProductManagement.App.Products.DeleteProduct;

public record DeleteProductByIdCommand(Guid Id) : IRequest;
    