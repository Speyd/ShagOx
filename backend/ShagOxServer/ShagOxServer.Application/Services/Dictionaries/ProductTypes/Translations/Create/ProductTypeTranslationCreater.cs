using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Create;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Create;
public static class ProductTypeTranslationCreater
{
    public static ProductTypeTranslation Create(
      ProductTypeTranslationCreateRequest request)
    {
        return new ProductTypeTranslation
        {
            TranslatableId = request.TranslatableId,
            Language = request.Language,
            Name = request.Name
        };
    }
}