using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Baskets;
public class BasketItem
    : BaseEntity
{
    public int BasketId { get; set; }

    public int AdvertisementId { get; set; }

    public int Quantity { get; set; }


    public override string ToString()
    {
        return $"Bask.: {BasketId} | Advert.: {AdvertisementId} | Quant.: {Quantity}";
    }
}