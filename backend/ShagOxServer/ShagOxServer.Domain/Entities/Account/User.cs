using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Domain.Entities.Account;
public class User : BaseEntity
{
    public string? Surname { get; set; }
    public string? Name { get; set; }

    public string PasswordHash { get; set; } = "";

    public string? Phone { get; set; }
    public string? Email { get; set; }

    public int? AvatarId { get; set; }
    public Avatar? Avatar { get; set; } = null;

    public int? CityId { get; set; }
    public City? City { get; set; } = null;

    public DateTime? LastSeenAt { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public Basket Basket { get; set; } = null!;

    public List<UserRole> UserRoles { get; set; } = new();

    public List<Advertisement> SoldAdvertisements { get; set; } = new();

    public List<Advertisement> BoughtAdvertisements { get; set; } = new();

    public List<Favorite> Favorites { get; set; } = new();

    public override string ToString()
    {
        return $"{Name} {Surname}";
    }
}
