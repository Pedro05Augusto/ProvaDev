using AutoMapper;
using MediatR;
using ProvaDev.Application.DTOs;
using ProvaDev.Domain.Models.Customers;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (customer == null)
            throw new KeyNotFoundException($"Cliente com ID {request.Id} não encontrado");

        customer.UpdateCustomer(new CustomerModel
        {
            Name = request.Name,
            Email = request.Email,
            Telephone = request.Telephone
        });

        await _repository.UpdateAsync(customer, cancellationToken);

        return _mapper.Map<CustomerDto>(customer);
    }
}
