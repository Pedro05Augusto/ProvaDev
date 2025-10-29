using MediatR;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<CustomerDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
}
