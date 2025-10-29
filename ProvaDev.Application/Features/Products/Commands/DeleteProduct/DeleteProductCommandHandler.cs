using MediatR;
using ProvaDev.Application.Common.Models;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (product == null)
            throw new KeyNotFoundException($"Produto com ID {request.Id} não encontrado");

        await _repository.DeleteAsync(request.Id, cancellationToken);

        return Result.SuccessResult($"Produto {product.Name} deletado com sucesso");
    }
}
