using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Update;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Update;
public static class ProductTypeTranslationUpdater
{
    public static int ApplyUpdates(
        ProductTypeTranslation type,
        ProductTypeTranslationUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.TranslatableId.HasValue)
        {
            type.TranslatableId = request.TranslatableId.Value;
            countUpdated++;
        }

        if (request.Language is not null)
        {
            type.Language = request.Language;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            type.Name = request.Name;
            countUpdated++;
        }

        return countUpdated;
    }
}