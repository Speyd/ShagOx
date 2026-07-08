using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Entities.Specification;

public class Condition : BaseEntity
{
    public string Name { get; set; } = "";

    public List<Advertisement> Advertisements { get; set; }
        = new List<Advertisement>();

    public override string ToString()
    {
        return Name;
    }
}
