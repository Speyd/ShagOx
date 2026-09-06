using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Domain.Entities.Specification;

public class Condition : BaseEntity
{
    public string Name { get; set; } = "";

    public List<Advertisement> Advertisements { get; set; } = new();

    public List<ConditionTranslation> Translations { get; set; }= new();


    public override string ToString()
    {
        return Name;
    }
}
