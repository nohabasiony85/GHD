using ProductManagement.App.Models;
using ProductManagement.Domain;

namespace ProductManagement.App.Products.GetProduct;

public record GetProductByIdQueryResponse
{
    public ProductDto Product { get; init; }

    public static GetProductByIdQueryResponse CopyFrom(Product product)
    {
        return new GetProductByIdQueryResponse
        {
            Product = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                Price = product.Price
            }
        };
    }
}