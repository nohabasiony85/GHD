using MediatR;

namespace ProductManagement.App.Products.GetProduct;

public record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdQueryResponse>;