using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Domain.Entities.Specification;
public class Condition 
    : BaseTranslatable,
    IEntityCache<Condition, ConditionCacheContext>
{
    public string Code { get; set; } = "";

    public ICollection<Advertisement> Advertisements { get; set; }
        = [];

    public ICollection<ConditionTranslation> Translations { get; set; }
        = [];


    public ConditionCacheContext GetCacheContext()
    {
        return new ConditionCacheContext(
            Id, EntityStatus);
    }

    public override string GetIdentificator()
    {
        return Code;
    }

    public override string ToString()
    {
        return Code;
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
