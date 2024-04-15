using ProductManagement.App.Exceptions;
using ProductManagement.App.Products.UpdateProduct;
using ProductManagement.Domain;

namespace ProductManagement.Tests
{
    public class UpdateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenProductUpdated()
        {
            // Arrange
            var productId = Guid.NewGuid();
            const string updatedProductName = "Updated Product";
            const string updatedProductBrand = "Updated Brand";
            const decimal updatedProductPrice = 20.5m;

            var productRepositoryMock = Substitute.For<IProductRepository>();
            var loggerMock = Substitute.For<ILogger<UpdateProductCommandHandler>>();

            var handler = new UpdateProductCommandHandler(productRepositoryMock, loggerMock);

            UpdateProductCommand request = new(
                productId,
                updatedProductName,
                updatedProductBrand,
                updatedProductPrice
            );

            var existingProduct = new Product
            {
                Id = productId,
                Name = "Original Product",
                Brand = "Original Brand",
                Price = 10.5m
            };

            productRepositoryMock.GetByIdAsync(productId, CancellationToken.None).Returns(existingProduct);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            await productRepositoryMock.Received(1).UpdateAsync(Arg.Is<Product>(
                p => p.Id == productId && p.Name == updatedProductName && p.Brand == updatedProductBrand &&
                     p.Price == updatedProductPrice), CancellationToken.None);

            
            response.Should().NotBeNull();
            response.ProductId.Should().Be(productId);
            response.Name.Should().Be(updatedProductName);
            response.Brand.Should().Be(updatedProductBrand);
            response.Price.Should().Be(updatedProductPrice);
        }

        [Fact]
        public async Task Handle_Should_ReturnsException_WhenProductUpdateFailed()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productRepositoryMock = Substitute.For<IProductRepository>();
            var loggerMock = Substitute.For<ILogger<UpdateProductCommandHandler>>();

            var handler = new UpdateProductCommandHandler(productRepositoryMock, loggerMock);

            UpdateProductCommand request = new(
                productId,
                "Updated Product",
                "Updated Brand",
                20.5m);

            productRepositoryMock.GetByIdAsync(productId, CancellationToken.None)!
                .Returns(Task.FromResult<Product>(null!));

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
