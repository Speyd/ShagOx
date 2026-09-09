using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Domain.Entities.Dictionaries;
public class Category : BaseEntity
{
    public string Code { get; set; } = null!;

    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; } = null!;


    public List<AttributeDefinition> Attributes { get; set; }
        = new List<AttributeDefinition>();

    public List<Advertisement> Advertisements { get; set; }
        = new List<Advertisement>();

    public List<CategoryTranslation> Translations { get; set; }
        = new List<CategoryTranslation>();


    public override string ToString()
    {
        return Code;
    }
}