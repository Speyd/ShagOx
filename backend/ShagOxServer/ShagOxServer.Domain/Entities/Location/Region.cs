using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Location;
public class Region : BaseEntity
{
    public string Name { get; set; } = "";
    public List<City> Cities { get; set; } = new List<City>();

    public override string ToString()
    {
        return Name;
    }
}
