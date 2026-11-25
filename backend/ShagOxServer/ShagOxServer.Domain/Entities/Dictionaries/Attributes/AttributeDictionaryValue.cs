using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Domain.Entities.Dictionaries.Attributes;
public class AttributeDictionaryValue
    : BaseTranslatable,
    IEntityCache<AttributeDictionaryValue,
        AttributeDictionaryValueCacheContext>
{
    public long DictionaryId { get; set; }
    public AttributeDictionary Dictionary { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Value { get; set; } = null;

    public ICollection<AttributeDictionaryValueTranslation> Translations { get; set; }
        = [];

    public AttributeDictionaryValueCacheContext GetCacheContext()
    {
        return new AttributeDictionaryValueCacheContext(
            Id, DictionaryId, EntityStatus);
    }

    public override string GetIdentificator()
    {
        return Code;
    }

    public override string ToString()
    {
        return $"Code: {Code} | Value: {Value ?? "Nan"}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
