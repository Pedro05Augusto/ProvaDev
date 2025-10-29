using MediatR;
using ProvaDev.Application.Common.Models;

namespace ProvaDev.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<Result>
{
    public int Id { get; set; }
}
