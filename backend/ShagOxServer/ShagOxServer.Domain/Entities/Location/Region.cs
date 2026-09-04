using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Domain.Entities.Location;
public class Region : BaseEntity
{
    public string Code { get; set; } = null!;

    public List<City> Cities { get; set; } = new List<City>();
    public List<RegionTranslation> Translations { get; set; } = new();


    public override string ToString()
    {
        return $"{Code}";
    }
}