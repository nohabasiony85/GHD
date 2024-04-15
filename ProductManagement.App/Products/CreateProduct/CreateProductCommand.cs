using MediatR;

namespace ProductManagement.App.Products.CreateProduct; 

public record CreateProductCommand(string Name, string Brand, decimal Price)
        : IRequest<CreateProductCommandResponse>;