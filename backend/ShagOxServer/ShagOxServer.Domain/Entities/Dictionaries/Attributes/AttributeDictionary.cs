using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Dictionaries.Attributes;
public class AttributeDictionary
    : BaseEntity
{
    public string Code { get; set; } = null!;

    public ICollection<AttributeDefinition> Attributes { get; set; }
        = [];

    public ICollection<AttributeDictionaryValue> Values { get; set; }
        = [];

    public override string ToString()
    {
        return $"Code: {Code}";
    }
}