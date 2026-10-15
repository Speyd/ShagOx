using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Entities.Specification.Pictures;
public class AdvertisementImage 
    : BasePictureOrder<Advertisement>
{
    public override string ToString()
    {
        return $"Order: {Order} | Url: {Url}";
    }
}
