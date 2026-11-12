using ShagOxServer.Domain.Base.Items;

namespace ShagOxServer.Domain.Entities.Baskets;
public class BasketItem
    : LineItem<Basket>
{

    public override string ToString()
    {
        return $"Bask.: {ParentId} | AdvertVariant: {AdvertisementVariantId} | Quant.: {Quantity}";
    }
}
