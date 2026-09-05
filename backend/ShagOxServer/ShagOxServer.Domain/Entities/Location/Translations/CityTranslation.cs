using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Location.Translations;
public class CityTranslation : BaseEntity
{
    public int CityId { get; set; }
    public City City { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string Language { get; set; } = null!;


    public override string ToString()
    {
        return $"{Name}";
    }
}