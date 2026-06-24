using ShagOxServer.Domain.Base;
namespace ShagOxServer.Domain.Entities.Specification;

public class Currency : BaseEntity
{
    public string Code { get; set; } = "";
    public string Symbol { get; set; } = "";
    public string Name { get; set; } = "";

    public List<Advertisement> Advertisements { get; set; } 
        = new List<Advertisement>();

    public override string ToString()
    {
        return Name;
    }
}
