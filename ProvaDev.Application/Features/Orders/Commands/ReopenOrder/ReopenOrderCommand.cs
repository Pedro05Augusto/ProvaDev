using MediatR;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Orders.Commands.ReopenOrder;

public class ReopenOrderCommand : IRequest<OrderDto>
{
    public int OrderId { get; set; }
}
