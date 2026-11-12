using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Entities.LineItems;
public abstract class LineItem
    : BaseEntity
{
    public long ParentId { get; set; }

    public long AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; } = null!;

    public long? AdvertisementVariantId { get; set; }
    public AdvertisementVariant? AdvertisementVariant { get; set; }


    public int Quantity { get; set; }
}
