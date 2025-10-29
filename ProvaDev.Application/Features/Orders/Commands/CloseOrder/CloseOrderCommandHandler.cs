using AutoMapper;
using MediatR;
using ProvaDev.Application.DTOs;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Orders.Commands.CloseOrder;

public class CloseOrderCommandHandler : IRequestHandler<CloseOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public CloseOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(CloseOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
            throw new KeyNotFoundException($"Pedido com ID {request.OrderId} não encontrado");

        order.Close();

        await _orderRepository.UpdateAsync(order, cancellationToken);

        var updatedOrder = await _orderRepository.GetByIdAsync(order.Id, cancellationToken);
        return _mapper.Map<OrderDto>(updatedOrder);
    }
}
