using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Entities.Baskets;
public class BasketItem
    : BaseEntity
{
    public Basket Basket { get; set; } = null!;
    public int BasketId { get; set; }

    public Advertisement Advertisement { get; set; } = null!;
    public int AdvertisementId { get; set; }

    public int Quantity { get; set; }


    public override string ToString()
    {
        return $"Bask.: {BasketId} | Advert.: {AdvertisementId} | Quant.: {Quantity}";
    }
}