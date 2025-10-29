using MediatR;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Orders.Commands.UpdateProductQuantity;

public class UpdateProductQuantityCommand : IRequest<OrderDto>
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int NewQuantity { get; set; }
}
