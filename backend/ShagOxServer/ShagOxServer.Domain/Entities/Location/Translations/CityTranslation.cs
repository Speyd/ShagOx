using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Location.Translations;
public class CityTranslation : BaseEntity
{
    public string Name { get; set; } = "";

    public int CityId { get; set; }
    public City Region { get; set; } = null!;

    public override string ToString()
    {
        return $"{Name}";
    }
}