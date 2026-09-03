using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Location.Translations;
public class RegionTranslation : BaseEntity
{
    public int RegionId { get; set; }
    public Region Region { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string Name { get; set; } = null!;


    public override string ToString()
    {
        return $"{Name}";
    }
}