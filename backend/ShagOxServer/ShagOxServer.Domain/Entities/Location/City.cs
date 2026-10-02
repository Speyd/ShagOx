using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Domain.Entities.Location;
public class City
    : BaseTranslatable
{
    public string Code { get; set; } = "";

    public long RegionId { get; set; }
    public Region Region { get; set; } = null!;

    public ICollection<User> Users { get; set; } 
        = [];
    public ICollection<CityTranslation> Translations { get; set; }
        = [];


    public override string GetIdentificator()
    {
        return Code;
    }

    public override string ToString()
    {
        return $"{Code}";
    }
}