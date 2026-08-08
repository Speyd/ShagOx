using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Advertisements;
public class Status : BaseEntity
{
    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Name { get; set; } = null!;

    public List<Advertisement> Advertisements { get; set; } = new();


    public override string ToString()
    {
        return $"{Code} - {Name}";
    }
}