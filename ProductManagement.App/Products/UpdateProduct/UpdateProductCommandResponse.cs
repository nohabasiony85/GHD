namespace ProductManagement.App.Products.UpdateProduct;

public record UpdateProductCommandResponse(Guid ProductId, string Name, string Brand, decimal Price);