using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Mapping;
public static class CategoryTranslationMapper
{
    public static CategoryTranslationDto ToDto(
        CategoryTranslation x)
    {
        return new CategoryTranslationDto
        (
            x.Id,
            x.Name
        );
    }
}