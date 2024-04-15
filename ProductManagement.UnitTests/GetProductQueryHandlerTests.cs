using ProductManagement.App.Exceptions;
using ProductManagement.App.Products.GetProduct;
using ProductManagement.Domain;

namespace ProductManagement.Tests;

public class GetProductQueryHandlerTests
{
    public class GetProductByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenProductFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Name = "Test Product",
                Brand = "Test Brand",
                Price = 10.5m
            };

            var productRepositoryMock = Substitute.For<IProductRepository>();
            productRepositoryMock.GetByIdAsync(productId).Returns(product);

            var request = new GetProductByIdQuery(productId);
            var handler = new GetProductByIdQueryHandler(productRepositoryMock);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Product.Id.Should().Be(productId);
            response.Product.Name.Should().Be("Test Product");
            response.Product.Brand.Should().Be("Test Brand");
            response.Product.Price.Should().Be(10.5m);
        }

        [Fact]
        public async Task Handle_Should_ReturnsNotFoundException_WhenProductFoundFailed()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productRepositoryMock = Substitute.For<IProductRepository>();
            productRepositoryMock.GetByIdAsync(productId)!.Returns(Task.FromResult<Product>(null!));

            var handler = new GetProductByIdQueryHandler(productRepositoryMock);
            var request = new GetProductByIdQuery(productId);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
