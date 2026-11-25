using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Location;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Domain.Entities.Location;
public class Region 
    : BaseTranslatable,
    IEntityCache<Region, RegionCacheContext>
{
    public string Code { get; set; } = null!;

    public ICollection<City> Cities { get; set; } 
        = [];
    public ICollection<RegionTranslation> Translations { get; set; }
        = [];


    public RegionCacheContext GetCacheContext()
    {
        return new RegionCacheContext(
            Id, EntityStatus);
    }

    public override string GetIdentificator()
    {
        return Code;
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
