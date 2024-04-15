namespace ProductManagement.App.Models;

public class ProductDto
{
    public Guid Id { get; init; }
        
    public required string Name { get; init; }
        
    public required string Brand { get; init; }
        
    public decimal Price { get; init; }
}