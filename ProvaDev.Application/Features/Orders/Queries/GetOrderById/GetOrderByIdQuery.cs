using MediatR;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQuery : IRequest<OrderDto?>
{
    public int Id { get; set; }
}
