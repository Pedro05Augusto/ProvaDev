using AutoMapper;
using MediatR;
using ProvaDev.Application.DTOs;
using ProvaDev.Domain.Entities.Customers;
using ProvaDev.Domain.Models.Customers;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(new CustomerModel
        {
            Name = request.Name,
            Email = request.Email,
            Telephone = request.Telephone
        });

        await _repository.AddAsync(customer, cancellationToken);
        return _mapper.Map<CustomerDto>(customer);
    }
}
