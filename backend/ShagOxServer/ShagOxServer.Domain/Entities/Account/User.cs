using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Domain.Entities.Account;
public class User : BaseEntity
{
    public string? Surname { get; set; }
    public string? Name { get; set; }

    public string PasswordHash { get; set; } = "";

    public string? Phone { get; set; }
    public string? Email { get; set; }

    public string? Avatar { get; set; }

    public int? CityId { get; set; }
    public City? City { get; set; } = null;

    public DateTime? LastSeenAt { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public List<Advertisement> SoldAdvertisements { get; set; } = new();

    public List<Advertisement> BoughtAdvertisements { get; set; } = new();

    public override string ToString()
    {
        return $"{Name} {Surname}";
    }
}
