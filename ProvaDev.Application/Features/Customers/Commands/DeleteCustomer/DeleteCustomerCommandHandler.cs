using MediatR;
using ProvaDev.Application.Common.Models;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result>
{
    private readonly ICustomerRepository _repository;

    public DeleteCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (customer == null)
            throw new KeyNotFoundException($"Cliente com ID {request.Id} não encontrado");

        await _repository.DeleteAsync(request.Id, cancellationToken);

        return Result.SuccessResult($"Cliente {customer.Name} deletado com sucesso");
    }
}
