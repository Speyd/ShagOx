using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Domain.Entities.Account;
public class User : BaseEntity
{
    public string Surname { get; set; } = "";
    public string Name { get; set; } = "";

    public string PasswordHash { get; set; } = "";

    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";

    public string Avatar { get; set; } = "";

    public required City City { get; set; }

    public DateTime? LastSeenAt { get; set; }
    public DateTime RegisteredAt { get; set; }

    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public List<Advertisement> Advertisements { get; set; }
        = new List<Advertisement>();

    public override string ToString()
    {
        return $"{Name} {Surname}";
    }
}
