using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Domain.Entities.Dictionaries.Attributes;
public class AttributeDefinition 
    : BaseEntity
{
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public string Key { get; set; } = null!;
    public AttributeType Type { get; set; }
    public bool Required { get; set; }

    public decimal? Min { get; set; }
    public decimal? Max { get; set; }

    public bool IsVariant { get; set; }
    public bool Multiple { get; set; }

    public ICollection<AttributeDefinitionTranslation> Translations { get; set; }
       = [];

    public BasketAttribute? BasketAttribute { get; set; }


    public override string ToString()
    {
        return $"{Key}(type: {Type} | req: {Required} | min: {Min} | max: {Max})";
    }
}
