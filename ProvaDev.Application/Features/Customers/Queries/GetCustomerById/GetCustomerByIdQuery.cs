using MediatR;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQuery : IRequest<CustomerDto?>
{
    public int Id { get; set; }
}
