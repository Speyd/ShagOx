using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Enum;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Domain.Entities.Advertisements;
public class Status
    : BaseTranslatable,
    IEntityCache<Status, StatusCacheContext>
{
    public AdvertisementStatus Code { get; set; }

    public ICollection<Advertisement> Advertisements { get; set; } 
        = [];

    public ICollection<StatusTranslation> Translations { get; set; }
        = [];


    public StatusCacheContext GetCacheContext()
    {
        return new StatusCacheContext(
            Id, EntityStatus);
    }

    public override string GetIdentificator()
    {
        return Code.ToString();
    }

    public override string ToString()
    {
        return $"{Code}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
