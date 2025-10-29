using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProvaDev.Application.Features.Orders.Commands.CreateOrder;
using ProvaDev.Application.Features.Orders.Commands.AddProductToOrder;
using ProvaDev.Application.Features.Orders.Commands.RemoveProductFromOrder;
using ProvaDev.Application.Features.Orders.Commands.UpdateProductQuantity;
using ProvaDev.Application.Features.Orders.Commands.CloseOrder;
using ProvaDev.Application.Features.Orders.Commands.ReopenOrder;
using ProvaDev.Application.Features.Orders.Commands.DeleteOrder;
using ProvaDev.Application.Features.Orders.Queries.GetOrderById;
using ProvaDev.Application.Features.Orders.Queries.GetAllOrders;
using ProvaDev.Application.Features.Orders.Queries.GetOpenOrders;

namespace ProvaDev.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Application.Common.Models.PaginatedList<Application.DTOs.OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetAllOrdersQuery { PageNumber = pageNumber, PageSize = pageSize };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("open")]
    [ProducesResponseType(typeof(Application.Common.Models.PaginatedList<Application.DTOs.OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOpen([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetOpenOrdersQuery { PageNumber = pageNumber, PageSize = pageSize };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Application.DTOs.OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetOrderByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound(new { message = $"Pedido com ID {id} não encontrado" });

        return Ok(result);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Application.DTOs.OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    
    [HttpPost("{id}/items")]
    [ProducesResponseType(typeof(Application.DTOs.OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddProduct(int id, [FromBody] AddProductToOrderCommand command)
    {
        if (id != command.OrderId)
            return BadRequest(new { message = "O ID da URL não corresponde ao OrderId do corpo da requisição" });

        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{id}/items/{productId}")]
    [ProducesResponseType(typeof(Application.DTOs.OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveProduct(int id, int productId)
    {
        var command = new RemoveProductFromOrderCommand { OrderId = id, ProductId = productId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPut("{id}/items/{productId}")]
    [ProducesResponseType(typeof(Application.DTOs.OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProductQuantity(int id, int productId, [FromBody] UpdateProductQuantityCommand command)
    {
        if (id != command.OrderId || productId != command.ProductId)
            return BadRequest(new { message = "Os IDs da URL não correspondem aos IDs do corpo da requisição" });

        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPut("{id}/close")]
    [ProducesResponseType(typeof(Application.DTOs.OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CloseOrder(int id)
    {
        var command = new CloseOrderCommand { OrderId = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPut("{id}/reopen")]
    [ProducesResponseType(typeof(Application.DTOs.OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReopenOrder(int id)
    {
        var command = new ReopenOrderCommand { OrderId = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Application.Common.Models.Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteOrderCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}