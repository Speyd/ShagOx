using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Account;
public class UserRole : BaseEntity
{
    public int UserId { get; set; }
    public required User User { get; set; }

    public int RoleId { get; set; }
    public required Role Role { get; set; }


    public override string ToString()
    {
        return $"{User?.Name} | {Role?.Name}";
    }
}
