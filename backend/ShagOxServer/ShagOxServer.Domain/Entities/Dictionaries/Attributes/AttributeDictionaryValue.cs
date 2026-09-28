using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Domain.Entities.Dictionaries.Attributes;
public class AttributeDictionaryValue
    : BaseTranslatable
{
    public long DictionaryId { get; set; }
    public AttributeDictionary Dictionary { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Value { get; set; } = null;

    public ICollection<AttributeDictionaryValueTranslation> Translations { get; set; }
        = [];


    public override string GetIdentificator()
    {
        return Code;
    }

    public override string ToString()
    {
        return $"Code: {Code} | Value: {Value ?? "Nan"}";
    }
}