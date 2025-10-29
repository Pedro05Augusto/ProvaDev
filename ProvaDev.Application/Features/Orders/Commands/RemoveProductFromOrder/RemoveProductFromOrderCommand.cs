using MediatR;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Orders.Commands.RemoveProductFromOrder;

public class RemoveProductFromOrderCommand : IRequest<OrderDto>
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
}
