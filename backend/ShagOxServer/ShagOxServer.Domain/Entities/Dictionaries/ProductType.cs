using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Domain.Entities.Dictionaries;
public class ProductType 
    : BaseTranslatable,
    IEntityCache<ProductType, ProductTypeCacheContext>
{
    public string Code { get; set; } = null!;
    public string Description { get; set; } = "";

    public ICollection<Category> Categories { get; set; }
        = [];

    public ICollection<ProductTypeTranslation> Translations { get; set; }
        = [];


    public ProductTypeCacheContext GetCacheContext()
    {
        return new ProductTypeCacheContext(
            Id, EntityStatus);
    }

    public override string GetIdentificator()
    {
        return Code;
    }

    public override string ToString()
    {
        return Code;
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
