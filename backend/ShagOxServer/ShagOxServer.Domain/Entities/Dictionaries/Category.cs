using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Dictionaries;
public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public ProductType ProductType { get; set; }

    public List<AttributeDefinition> Attributes { get; set; }
        = new List<AttributeDefinition>();

    public List<Advertisement> Advertisements { get; set; }
        = new List<Advertisement>();

    public override string ToString()
    {
        return Name;
    }
}