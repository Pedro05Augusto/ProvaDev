using MediatR;
using ProvaDev.Application.Common.Models;

namespace ProvaDev.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommand : IRequest<Result>
{
    public int Id { get; set; }
}
