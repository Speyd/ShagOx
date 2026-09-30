using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Domain.Entities.Dictionaries;
public class Category 
    : BaseTranslatable
{
    public string Code { get; set; } = null!;

    public long ProductTypeId { get; set; }
    public ProductType ProductType { get; set; } = null!;


    public ICollection<AttributeDefinition> Attributes { get; set; }
        = [];

    public ICollection<Advertisement> Advertisements { get; set; }
       = [];

    public List<CategoryTranslation> Translations { get; set; }
       = [];


    public override string GetIdentificator()
    {
        return Code;
    }

    public override string ToString()
    {
        return Code;
    }
}