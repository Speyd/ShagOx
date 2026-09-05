using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Domain.Entities.Location;
public class City : BaseEntity
{
    public string Code { get; set; } = "";

    public int RegionId { get; set; }
    public Region Region { get; set; } = null!;

    public List<User> Users { get; set; } = new List<User>();
    public List<CityTranslation> Translations { get; set; } = new();


    public override string ToString()
    {
        return $"{Code}";
    }
}