using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Dictionaries.Translations;
public class ProductTypeTranslation
    : BaseTranslation<ProductType>
{
    public string Name { get; set; } = null!;

    public override string ToString()
    {
        return $"{Name}";
    }
}