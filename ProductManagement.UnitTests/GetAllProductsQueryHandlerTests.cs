using ProductManagement.App.Products.GetAllProduct;
using ProductManagement.Domain;

namespace ProductManagement.Tests;

public class GetAllProductsQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenAllProductsFound()
    {
        // Arrange
        var productRepositoryMock = Substitute.For<IProductRepository>();
        var handler = new GetAllProductsQueryHandler(productRepositoryMock);
        var request = new GetAllProductsQuery();

        var expectedProducts = new List<Product>
        {
            new() { Id = Guid.NewGuid(), Name = "Product 1", Brand = "Brand 1", Price = 10.5m },
            new() { Id = Guid.NewGuid(), Name = "Product 2", Brand = "Brand 2", Price = 20.5m },
            new() { Id = Guid.NewGuid(), Name = "Product 3", Brand = "Brand 3", Price = 30.5m }
        };

        productRepositoryMock.GetAllAsync(CancellationToken.None).Returns(expectedProducts);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Products.Count.Should().Be(expectedProducts.Count);

        for (var i = 0; i < expectedProducts.Count; i++)
        {
            response.Products[i].Id.Should().Be(expectedProducts[i].Id);
            response.Products[i].Name.Should().Be(expectedProducts[i].Name);
            response.Products[i].Brand.Should().Be(expectedProducts[i].Brand);
            response.Products[i].Price.Should().Be(expectedProducts[i].Price);
        }
    }

    [Fact]
    public async Task Handle_Should_ReturnsNotFoundException_WhenAllProductsFoundFailed()
    {
        // Arrange
        var productRepositoryMock = Substitute.For<IProductRepository>();
        var handler = new GetAllProductsQueryHandler(productRepositoryMock);
        var request = new GetAllProductsQuery();

        productRepositoryMock.GetAllAsync(CancellationToken.None)
            .Throws(new Exception("Simulated repository exception"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
    }
}
