using AutoMapper;
using MediatR;
using ProvaDev.Application.Common.Models;
using ProvaDev.Application.DTOs;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Orders.Queries.GetOpenOrders;

public class GetOpenOrdersQueryHandler : IRequestHandler<GetOpenOrdersQuery, PaginatedList<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetOpenOrdersQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OrderDto>> Handle(GetOpenOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repository.GetOpenOrdersAsync(cancellationToken);
        var orderDtos = _mapper.Map<List<OrderDto>>(orders);
        
        return PaginatedList<OrderDto>.Create(orderDtos, request.PageNumber, request.PageSize);
    }
}
