using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Location.Translations;
public class RegionTranslation 
    : BaseTranslation<Region>
{
    public string Name { get; set; } = null!;


    public override string GetIdentificator()
    {
        return Name;
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}