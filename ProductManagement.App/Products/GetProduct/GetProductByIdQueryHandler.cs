using MediatR;
using ProductManagement.App.Exceptions;
using ProductManagement.Domain;

namespace ProductManagement.App.Products.GetProduct;

public class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);

        if (product is null)
            throw new NotFoundException(nameof(Domain.Product), request.Id);

        return GetProductByIdQueryResponse.CopyFrom(product);
    }
}
