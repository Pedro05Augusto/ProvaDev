using MediatR;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<ProductDto?>
{
    public int Id { get; set; }
}
