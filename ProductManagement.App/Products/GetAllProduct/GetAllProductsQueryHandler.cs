using MediatR;
using ProductManagement.Domain;

namespace ProductManagement.App.Products.GetAllProduct;

public class GetAllProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetAllProductsQuery, GetAllProductsQueryResponse>
{
    public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(cancellationToken);

        return GetAllProductsQueryResponse.CopyFrom(products);
    }
}