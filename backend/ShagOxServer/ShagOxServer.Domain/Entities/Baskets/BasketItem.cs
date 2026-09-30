using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Entities.Baskets;
public class BasketItem
    : BaseEntity
{
    public Basket Basket { get; set; } = null!;
    public long BasketId { get; set; }

    public AdvertisementVariant AdvertisementVariant { get; set; } = null!;
    public long AdvertisementVariantId { get; set; }

    public int Quantity { get; set; }


    public override string ToString()
    {
        return $"Bask.: {BasketId} | AdvertVariant: {AdvertisementVariantId} | Quant.: {Quantity}";
    }
}