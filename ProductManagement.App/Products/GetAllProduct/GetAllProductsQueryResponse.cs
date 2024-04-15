using ProductManagement.App.Models;
using ProductManagement.Domain;

namespace ProductManagement.App.Products.GetAllProduct;

public record GetAllProductsQueryResponse
{
    public List<ProductDto> Products { get; init; }

    public static GetAllProductsQueryResponse CopyFrom(IReadOnlyList<Product> products)
    {
        return new GetAllProductsQueryResponse()
        {
            Products = products
                .Select(product => new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Brand = product.Brand,
                    Price = product.Price
                }).ToList()
        };
    }
}