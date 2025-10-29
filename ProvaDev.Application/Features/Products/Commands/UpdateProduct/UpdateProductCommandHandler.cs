using AutoMapper;
using MediatR;
using ProvaDev.Application.DTOs;
using ProvaDev.Domain.Models;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (product == null)
            throw new KeyNotFoundException($"Produto com ID {request.Id} não encontrado");

        product.UpdateProduct(new ProductModel
        {
            Name = request.Name,
            Price = request.Price
        });

        await _repository.UpdateAsync(product, cancellationToken);

        return _mapper.Map<ProductDto>(product);
    }
}
