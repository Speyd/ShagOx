using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Domain.Entities.Dictionaries;
public class ProductType : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Description { get; set; } = "";

    public List<Category> Categories { get; set; }
        = new List<Category>();

    public List<ProductTypeTranslation> Translations { get; set; }
        = new List<ProductTypeTranslation>();

    public override string ToString()
    {
        return Code;
    }
}