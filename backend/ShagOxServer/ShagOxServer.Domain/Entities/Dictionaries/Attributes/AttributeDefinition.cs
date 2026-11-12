using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Domain.Entities.Dictionaries.Attributes;
public class AttributeDefinition 
    : BaseTranslatable
{
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public long? DictionaryId { get; set; }
    public AttributeDictionary? Dictionary { get; set; }

    public string Key { get; set; } = null!;
    public AttributeType Type { get; set; }
    public bool Required { get; set; }

    public decimal? Min { get; set; }
    public decimal? Max { get; set; }

    public bool IsVariant { get; set; }
    public bool Multiple { get; set; }

    public LineItemAttribute? BasketAttribute { get; set; }

    public ICollection<AttributeDefinitionTranslation> Translations { get; set; }
       = [];


    public override string GetIdentificator()
    {
        return Key;
    }

    public override string ToString()
    {
        return $"{Key}(type: {Type} | req: {Required} | min: {Min} | max: {Max})";
    }
}
