using MediatR;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Orders.Commands.AddProductToOrder;

public class AddProductToOrderCommand : IRequest<OrderDto>
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
