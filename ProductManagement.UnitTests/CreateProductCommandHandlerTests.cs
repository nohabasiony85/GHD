using ProductManagement.App.Products.CreateProduct;
using ProductManagement.Domain;

namespace ProductManagement.Tests;

public class CreateProductCommandHandlerTests
{
    private readonly IProductRepository? _productRepositoryMock = Substitute.For<IProductRepository>();

    private readonly ILogger<CreateProductCommandHandler> _loggerMock =
        Substitute.For<ILogger<CreateProductCommandHandler>>();

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenProductCreated()
    {
        //Arrange
        var product = Guid.NewGuid();
        CreateProductCommand command = new(
            "test product",
            "test product brand",
            200);


        var handler = new CreateProductCommandHandler(_productRepositoryMock, _loggerMock);
        // Act
        var result = await handler.Handle(command);

        // Assert
        result.Should().NotBeNull();
        result.ProductId.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_ReturnsException_WhenProductCreatedFailed()
    {
        //Arrange
        var request = new CreateProductCommand("Test Product", "Test Brand", 10.5m);
        var handler = new CreateProductCommandHandler(_productRepositoryMock, _loggerMock);

        _productRepositoryMock
            .When(repo => repo.AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()))
            .Do(_ => throw new Exception("Simulated repository exception"));

        // Act
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));

        // Assert
        _loggerMock.Received().LogError(Arg.Any<Exception>(), "Failed to create product");
    }
}