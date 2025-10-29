using MediatR;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<OrderDto>
{
    public int CustomerId { get; set; }
}
