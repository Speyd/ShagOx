using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Account;
public class UserRole 
    : BaseEntity
{
    public long UserId { get; set; }
    public User User { get; set; } = null!;

    public long RoleId { get; set; }
    public Role Role { get; set; } = null!;


    public override string ToString()
    {
        return $"{User?.UserName} | {Role?.Name}";
    }
}
