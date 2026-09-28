using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Baskets;
using System.Text.Json;

namespace ShagOxServer.Domain.Entities.Advertisements;
public class AdvertisementVariant
    : BaseEntity
{
    public long AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; } = null!;

    /// <summary>
    /// Current product price in the specified currency.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Previous product price used for displaying discounts and price changes.
    /// </summary>
    public decimal PreviousPrice { get; set; }

    public int Stock { get; set; }

    public JsonDocument Attributes { get; set; } = null!;

    public ICollection<BasketItem> BasketItems { get; set; }
        = [];


    public override string ToString()
    {
        return $"Price: {Price} | Stock: {Stock}";
    }
}