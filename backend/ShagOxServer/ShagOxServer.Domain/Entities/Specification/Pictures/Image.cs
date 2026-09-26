using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Entities.Specification.Pictures;
public class Image : BaseImage
{
    public int Order { get; set; }

    public long AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; } = null!;


    public override string ToString()
    {
        return $"Order: {Order} | Url: {Url}";
    }
}