using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Update;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Update;
public static class CategoryTranslationUpdater
{
    public static int ApplyUpdates(
        CategoryTranslation category,
        CategoryTranslationUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.TranslatableId.HasValue)
        {
            category.TranslatableId = request.TranslatableId.Value;
            countUpdated++;
        }

        if (request.Language is not null)
        {
            category.Language = request.Language;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            category.Name = request.Name;
            countUpdated++;
        }

        return countUpdated;
    }
}