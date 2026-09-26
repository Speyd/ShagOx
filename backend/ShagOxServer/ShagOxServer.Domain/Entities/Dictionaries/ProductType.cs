using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Domain.Entities.Dictionaries;
public class ProductType : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Description { get; set; } = "";

    public ICollection<Category> Categories { get; set; }
        = [];

    public ICollection<ProductTypeTranslation> Translations { get; set; }
        = [];

    public override string ToString()
    {
        return Code;
    }
}