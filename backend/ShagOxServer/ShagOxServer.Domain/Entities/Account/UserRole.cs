using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Account;
public class UserRole : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;


    public override string ToString()
    {
        return $"{User?.Name} | {Role?.Name}";
    }
}
