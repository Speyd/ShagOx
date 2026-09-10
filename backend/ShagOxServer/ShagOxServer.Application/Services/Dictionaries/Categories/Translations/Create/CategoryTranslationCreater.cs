using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Create;
public static class CategoryTranslationCreater
{
    public static CategoryTranslation Create(
      CategoryTranslationCreateRequest request)
    {
        return new CategoryTranslation
        {
            TranslatableId = request.TranslatableId,
            Language = request.Language,
            Name = request.Name
        };
    }
}