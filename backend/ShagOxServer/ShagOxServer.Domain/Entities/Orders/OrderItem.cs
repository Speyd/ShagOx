using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Entities.Orders;
public class OrderItem
    : BaseEntity
{
    public long OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public long AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; } = null!;

    public long? AdvertisementVariantId { get; set; }
    public AdvertisementVariant? AdvertisementVariant { get; set; }

    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Price => TotalPrice / Quantity;


    public override string ToString()
    {
        throw new NotImplementedException();
    }
}
