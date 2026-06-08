
using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Account;
public class Role : BaseEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public override string ToString()
    {
        return Name;
    }
}
