using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Specification;

public class Condition : BaseEntity
{
    public string Name { get; set; } = "";

    public override string ToString()
    {
        return Name;
    }
}
