using MediatR;
using ProvaDev.Application.Common.Models;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQuery : IRequest<PaginatedList<OrderDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
