using ProvaDev.Domain.Entities.Orders;

namespace ProvaDev.Domain.Tests.Entities.Orders;

public static class OrderFactory
{
    public static Order CreateValid(int customerId = 1)
    {
        return Order.Create(customerId);
    }
}