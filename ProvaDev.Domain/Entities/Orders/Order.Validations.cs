using ProvaDev.Domain.Common.Validators;
using ProvaDev.Domain.Common.Validators.ValidationExtensions;
using ProvaDev.Domain.Entities.Products;

namespace ProvaDev.Domain.Entities.Orders;

public partial class Order
{
    private List<Error> ValidateCanAddProduct(Product product)
    {
        return new List<Error>()
            .NotNull(product, nameof(product))
            .MustBeFalse(IsClosed, nameof(Order), "Não é possível adicionar produtos a um pedido fechado.");
    }

    private List<Error> ValidateCanRemoveProduct(int productId)
    {
        return new List<Error>()
            .MustBeFalse(IsClosed, nameof(Order), "Não é possível remover produtos de um pedido fechado.")
            .MustExist(Items.Any(i => i.ProductId == productId), nameof(productId), $"Produto {productId} não encontrado no pedido.");
    }

    private List<Error> ValidateCanUpdateQuantity(int productId, int newQuantity)
    {
        return new List<Error>()
            .MustBeFalse(IsClosed, nameof(Order), "Não é possível alterar quantidades em um pedido fechado.")
            .MustExist(Items.Any(i => i.ProductId == productId), nameof(productId), $"Produto {productId} não encontrado no pedido.");
    }

    private List<Error> ValidateCanClose()
    {
        return new List<Error>()
            .MustBeFalse(IsClosed, nameof(Order), "O pedido já está fechado.")
            .MustBeTrue(Items.Any(), nameof(Order), "Não é possível fechar um pedido sem itens.");
    }

    private List<Error> ValidateCanReopen()
    {
        return new List<Error>()
            .MustBeTrue(IsClosed, nameof(Order), "O pedido já está aberto.");
    }
}