using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Domain.Entities.Location;
public class Region : BaseEntity
{
    public string Code { get; set; } = null!;

    public ICollection<City> Cities { get; set; } 
        = [];
    public ICollection<RegionTranslation> Translations { get; set; }
        = [];


    public override string ToString()
    {
        return $"{Code}";
    }
}