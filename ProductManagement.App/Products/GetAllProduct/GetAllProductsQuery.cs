using MediatR;

namespace ProductManagement.App.Products.GetAllProduct;

public record GetAllProductsQuery: IRequest<GetAllProductsQueryResponse>;