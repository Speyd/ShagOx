using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Services.Base.Translations.Mapping;
public static class BaseTranslationCacheMapper
{
    public static BaseTranslationCacheInfo ToInfo<TTranslatable>(
        BaseTranslation<TTranslatable> translationEntity)
        where TTranslatable : BaseTranslatable
    {
        return new BaseTranslationCacheInfo(
            translationEntity.Id,
            translationEntity.Language
        );
    }
}