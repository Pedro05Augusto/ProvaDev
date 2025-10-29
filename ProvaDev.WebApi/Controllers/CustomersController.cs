using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProvaDev.Application.Features.Customers.Commands.CreateCustomer;
using ProvaDev.Application.Features.Customers.Commands.UpdateCustomer;
using ProvaDev.Application.Features.Customers.Commands.DeleteCustomer;
using ProvaDev.Application.Features.Customers.Queries.GetCustomerById;
using ProvaDev.Application.Features.Customers.Queries.GetAllCustomers;

namespace ProvaDev.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(Application.Common.Models.PaginatedList<Application.DTOs.CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetAllCustomersQuery { PageNumber = pageNumber, PageSize = pageSize };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Application.DTOs.CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetCustomerByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound(new { message = $"Cliente com ID {id} não encontrado" });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Application.DTOs.CustomerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Application.DTOs.CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerCommand command)
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
        var command = new DeleteCustomerCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}