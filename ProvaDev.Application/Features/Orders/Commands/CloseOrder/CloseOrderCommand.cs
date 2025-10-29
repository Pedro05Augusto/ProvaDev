using MediatR;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Orders.Commands.CloseOrder;

public class CloseOrderCommand : IRequest<OrderDto>
{
    public int OrderId { get; set; }
}
