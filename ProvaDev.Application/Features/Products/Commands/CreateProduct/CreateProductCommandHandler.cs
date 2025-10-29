using AutoMapper;
using MediatR;
using ProvaDev.Application.DTOs;
using ProvaDev.Domain.Entities.Products;
using ProvaDev.Domain.Models;
using ProvaDev.Domain.Repositories;

namespace ProvaDev.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(new ProductModel
        {
            Name = request.Name,
            Price = request.Price
        });

        await _repository.AddAsync(product, cancellationToken);

        return _mapper.Map<ProductDto>(product);
    }
}
