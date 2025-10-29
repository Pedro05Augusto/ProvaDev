using AutoMapper;
using MediatR;
using ProvaDev.Application.DTOs;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Orders.Commands.AddProductToOrder;

public class AddProductToOrderCommandHandler : IRequestHandler<AddProductToOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public AddProductToOrderCommandHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(AddProductToOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
            throw new KeyNotFoundException($"Pedido com ID {request.OrderId} não encontrado");
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Produto com ID {request.ProductId} não encontrado");

        order.AddProduct(product, request.Quantity);

        await _orderRepository.UpdateAsync(order, cancellationToken);
        var updatedOrder = await _orderRepository.GetByIdAsync(order.Id, cancellationToken);
        return _mapper.Map<OrderDto>(updatedOrder);
    }
}
