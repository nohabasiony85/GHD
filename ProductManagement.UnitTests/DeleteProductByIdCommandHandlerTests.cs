using ProductManagement.App.Products.DeleteProduct;
using ProductManagement.Domain;

namespace ProductManagement.Tests
{
    public class DeleteProductByIdCommandHandlerTests
    {
        private readonly IProductRepository? _productRepositoryMock = Substitute.For<IProductRepository>();

        private readonly ILogger<DeleteProductByIdCommandHandler> _loggerMock =
            Substitute.For<ILogger<DeleteProductByIdCommandHandler>>();

        [Fact]
        public async Task Handle_Should_ReturnsSuccess_ProductDeletedSuccessfully()
        {
            // Arrange
            var productId = Guid.NewGuid();

            var handler = new DeleteProductByIdCommandHandler(_productRepositoryMock, _loggerMock);
            var request = new DeleteProductByIdCommand(productId);

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            await _productRepositoryMock.Received(1).DeleteAsync(productId);
        }

        [Fact]
        public async Task Handle_Should_ReturnsException_WhenProductDeletionFailed()
        {
            // Arrange
            var productId = Guid.NewGuid();

            var handler = new DeleteProductByIdCommandHandler(_productRepositoryMock, _loggerMock);
            var request = new DeleteProductByIdCommand(productId);

            var expectedException = new Exception("Simulated repository exception");
            _productRepositoryMock.DeleteAsync(productId).Throws(expectedException);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
            expectedException.Should().Be(exception);
        }
    }
}