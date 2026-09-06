using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Specification.Translations;
public class ConditionTranslation
    : BaseTranslation<Condition>
{
    public string Name { get; set; } = null!;

    public override string ToString()
    {
        return $"{Name}";
    }
}