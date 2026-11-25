using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Location;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Domain.Entities.Location;
public class City
    : BaseTranslatable,
    IEntityCache<City, CityCacheContext>
{
    public string Code { get; set; } = "";

    public long RegionId { get; set; }
    public Region Region { get; set; } = null!;

    public ICollection<User> Users { get; set; } 
        = [];
    public ICollection<CityTranslation> Translations { get; set; }
        = [];


    public CityCacheContext GetCacheContext()
    {
        return new CityCacheContext(
            Id, RegionId, EntityStatus);
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
