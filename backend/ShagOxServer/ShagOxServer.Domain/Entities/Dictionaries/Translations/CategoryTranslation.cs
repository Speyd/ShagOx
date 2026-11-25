using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Translations;

namespace ShagOxServer.Domain.Entities.Dictionaries.Translations;
public class CategoryTranslation
    : BaseTranslation<Category>,
    IEntityCache<CategoryTranslation,
        TranslationCacheContext>
{
    public string Name { get; set; } = null!;

    public TranslationCacheContext GetCacheContext()
    {
        return new TranslationCacheContext(
            Id, Language);
    }

    public override string GetTranslationValue()
    {
        return Name;
    }

    public override string ToString()
    {
        return $"{Name}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
