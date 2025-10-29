using AutoMapper;
using MediatR;
using ProvaDev.Application.Common.Models;
using ProvaDev.Application.DTOs;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, PaginatedList<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetAllOrdersQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repository.GetAllAsync(cancellationToken);
        var orderDtos = _mapper.Map<List<OrderDto>>(orders);
        
        return PaginatedList<OrderDto>.Create(orderDtos, request.PageNumber, request.PageSize);
    }
}
