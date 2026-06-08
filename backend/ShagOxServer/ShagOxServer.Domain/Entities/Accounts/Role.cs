
using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Accounts;

public class Role : BaseEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    public override string ToString()
    {
        return Name;
    }
}
