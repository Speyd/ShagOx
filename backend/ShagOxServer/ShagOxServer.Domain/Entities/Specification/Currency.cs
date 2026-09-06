using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;
namespace ShagOxServer.Domain.Entities.Specification;

public class Currency : BaseEntity
{
    public string Code { get; set; } = "";
    public string Symbol { get; set; } = "";
    public string Name { get; set; } = "";

    public List<Advertisement> Advertisements { get; set; } = new();

    public override string ToString()
    {
        return Name;
    }
}
