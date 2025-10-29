using MediatR;
using ProvaDev.Application.Common.Models;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
