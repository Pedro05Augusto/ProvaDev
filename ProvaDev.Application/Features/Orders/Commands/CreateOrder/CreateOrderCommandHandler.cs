using AutoMapper;
using MediatR;
using ProvaDev.Application.DTOs;
using ProvaDev.Domain.Entities.Orders;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customerExists = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customerExists == null)
            throw new KeyNotFoundException($"Cliente com ID {request.CustomerId} não encontrado");
        var order = Order.Create(request.CustomerId);

        await _orderRepository.AddAsync(order, cancellationToken);

        var createdOrder = await _orderRepository.GetByIdAsync(order.Id, cancellationToken);
        
        return _mapper.Map<OrderDto>(createdOrder);
    }
}
