using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
public class AttributeDictionaryValueTranslation
    : BaseTranslation<AttributeDictionaryValue>
{
    public string Name { get; set; } = null!;

    public override string ToString()
    {
        return $"{Name}";
    }
}