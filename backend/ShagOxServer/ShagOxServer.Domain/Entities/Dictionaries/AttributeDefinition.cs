using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Domain.Entities.Dictionaries;
public class AttributeDefinition : BaseEntity
{
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public string Key { get; set; } = null!;
    public AttributeType Type { get; set; }
    public bool Required { get; set; }

    public int? Min { get; set; }
    public int? Max { get; set; }

    public List<AttributeDefinitionTranslation> Translations { get; set; } 
        = new();


    public override string ToString()
    {
        return $"{Key}(type: {Type} | req: {Required} | min: {Min} | max: {Max})";
    }
}
