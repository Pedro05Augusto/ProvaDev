using MediatR;
using ProvaDev.Application.DTOs;

namespace ProvaDev.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<ProductDto>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
