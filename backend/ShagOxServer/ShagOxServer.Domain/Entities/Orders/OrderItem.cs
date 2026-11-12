using ShagOxServer.Domain.Entities.LineItems.Core;

namespace ShagOxServer.Domain.Entities.Orders;
public class OrderItem
    : LineItem<Order>
{
    public decimal TotalWeight { get; set; }
    public decimal TotalPrice { get; set; }

    public decimal Price => Quantity > 0
        ? TotalPrice / Quantity
        : 0m;


    public override string ToString()
    {
        throw new NotImplementedException();
    }
}
