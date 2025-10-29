using AutoMapper;
using MediatR;
using ProvaDev.Application.Common.Models;
using ProvaDev.Application.DTOs;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, PaginatedList<CustomerDto>>
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCustomersQueryHandler(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _repository.GetAllAsync(cancellationToken);
        var customerDtos = _mapper.Map<List<CustomerDto>>(customers);
        
        return PaginatedList<CustomerDto>.Create(customerDtos, request.PageNumber, request.PageSize);
    }
}
