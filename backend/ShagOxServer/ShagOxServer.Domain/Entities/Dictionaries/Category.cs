using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Dictionaries;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Domain.Entities.Dictionaries;
public class Category 
    : BaseTranslatable,
    IEntityCache<Category, CategoryCacheContext>
{
    public string Code { get; set; } = null!;

    public long ProductTypeId { get; set; }
    public ProductType ProductType { get; set; } = null!;

    public long? CategoryImageId { get; set; }
    public CategoryImage? CategoryImage { get; set; }


    public ICollection<AttributeDefinition> Attributes { get; set; }
        = [];

    public ICollection<Advertisement> Advertisements { get; set; }
       = [];

    public List<CategoryTranslation> Translations { get; set; }
       = [];


    public CategoryCacheContext GetCacheContext()
    {
        return new CategoryCacheContext(
            Id, ProductTypeId, EntityStatus);
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
