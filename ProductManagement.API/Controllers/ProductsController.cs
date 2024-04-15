using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.App.Products.CreateProduct;
using ProductManagement.App.Products.GetAllProduct;
using ProductManagement.App.Products.GetProduct;
using ProductManagement.App.Products.UpdateProduct;
using ProductManagement.Domain;

namespace ProductManagement.API.Controllers;

[Route("api/products")]

[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase
{
    // GET: api/products/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetProductByIdQueryResponse>> GetById(Guid id)
    {
        var product = await mediator.Send(new GetProductByIdQuery(id));

        return product;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var query = new GetAllProductsQuery();

        var products = await mediator.Send(query);

        return Ok(products);
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
    {
        var response = await mediator.Send(command);

        var responseProductId = response.ProductId;

        return CreatedAtAction(nameof(GetById), new { id = responseProductId }, responseProductId);
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<GetProductByIdQueryResponse>> Update(Guid id, UpdateProductCommand command)
    {
        if (id != command.ProductId)
        {
            return BadRequest("The provided ID does not match the ID in the request body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await mediator.Send(command);

        return NoContent();
    }
}