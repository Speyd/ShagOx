using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Mapping;
public static class ProductTypeTranslationMapper
{
    public static ProductTypeTranslationDto ToDto(
        ProductTypeTranslation x)
    {
        return new ProductTypeTranslationDto
        (
            x.Id,
            x.Name
        );
    }
}