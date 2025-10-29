using MediatR;
using ProvaDev.Application.Common.Models;

namespace ProvaDev.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<Result>
{
    public int Id { get; set; }
}
