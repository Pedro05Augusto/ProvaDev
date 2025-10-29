using MediatR;
using ProvaDev.Application.Common.Models;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result>
{
    private readonly IOrderRepository _repository;

    public DeleteOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (order == null)
            throw new KeyNotFoundException($"Pedido com ID {request.Id} não encontrado");

        await _repository.DeleteAsync(request.Id, cancellationToken);

        return Result.SuccessResult($"Pedido #{order.Id} deletado com sucesso");
    }
}
