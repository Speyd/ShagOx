using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Specification;
public class Image : BaseEntity
{
    public string Url { get; set; } = "";
    public int Order { get; set; }

    public override string ToString()
    {
        return $"Order: {Order} | Url: {Url}";
    }
}