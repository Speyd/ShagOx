using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Domain.Entities.Specification;

public class Condition : BaseEntity
{
    public string Code { get; set; } = "";

    public ICollection<Advertisement> Advertisements { get; set; }
        = [];

    public ICollection<ConditionTranslation> Translations { get; set; }
        = [];


    public override string ToString()
    {
        return Code;
    }
}
