using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProvaDev.Application.Features.Products.Commands.CreateProduct;
using ProvaDev.Application.Features.Products.Commands.UpdateProduct;
using ProvaDev.Application.Features.Products.Commands.DeleteProduct;
using ProvaDev.Application.Features.Products.Queries.GetProductById;
using ProvaDev.Application.Features.Products.Queries.GetAllProducts;

namespace ProvaDev.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(Application.Common.Models.PaginatedList<Application.DTOs.ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetAllProductsQuery { PageNumber = pageNumber, PageSize = pageSize };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Application.DTOs.ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetProductByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound(new { message = $"Produto com ID {id} não encontrado" });

        return Ok(result);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Application.DTOs.ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Application.DTOs.ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { message = "O ID da URL não corresponde ao ID do corpo da requisição" });

        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Application.Common.Models.Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteProductCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}