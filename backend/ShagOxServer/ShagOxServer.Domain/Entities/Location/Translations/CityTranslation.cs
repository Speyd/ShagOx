using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Location.Translations;
public class CityTranslation 
    : BaseTranslation<City>
{
    public string Name { get; set; } = null!;

    public override string ToString()
    {
        return $"{Name}";
    }
}