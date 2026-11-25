using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;

namespace ShagOxServer.Domain.Entities.Dictionaries.Attributes;
public class AttributeDictionary
    : BaseEntity,
    IEntityCache<AttributeDictionary, AttributeDictionaryCacheContext>
{
    public string Code { get; set; } = null!;

    public ICollection<AttributeDefinition> Attributes { get; set; }
        = [];

    public ICollection<AttributeDictionaryValue> Values { get; set; }
        = [];

    public AttributeDictionaryCacheContext GetCacheContext()
    {
        return new AttributeDictionaryCacheContext(
            Id, EntityStatus);
    }

    public override string ToString()
    {
        return $"Code: {Code}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
