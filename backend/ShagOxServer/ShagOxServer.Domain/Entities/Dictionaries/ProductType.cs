using ShagOxServer.Domain.Base;
namespace ShagOxServer.Domain.Entities.Dictionaries;

public class ProductType : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = "";


    public override string ToString()
    {
        return Name;
    }
}