using MediatR;
using ProvaDev.Application.Common.Models;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQuery : IRequest<PaginatedList<CustomerDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
