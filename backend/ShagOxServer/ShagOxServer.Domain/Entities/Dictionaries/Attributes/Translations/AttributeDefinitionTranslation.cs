using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
public class AttributeDefinitionTranslation
    : BaseTranslation<AttributeDefinition>
{
    public string Name { get; set; } = null!;


    public override string GetTranslationValue()
    {
        return Name;
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}