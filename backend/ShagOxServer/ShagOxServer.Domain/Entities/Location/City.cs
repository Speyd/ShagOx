using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Location;

public class City : BaseEntity
{
    public string Name { get; set; } = "";
    public int RegionId { get; set; }
    public Region? Region { get; set; } = null;

    public override string ToString()
    {
        return $"{Name}(${(Region is null? "Unknow": Region.Name)})";
    }
}
