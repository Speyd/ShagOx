using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Entities.Specification;
public class Image : BaseEntity
{
    public string Url { get; set; } = "";
    public int Order { get; set; }

    public string PublicId { get; set; } = null!;

    public int AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; } = null!;

    public override string ToString()
    {
        return $"Order: {Order} | Url: {Url}";
    }
}