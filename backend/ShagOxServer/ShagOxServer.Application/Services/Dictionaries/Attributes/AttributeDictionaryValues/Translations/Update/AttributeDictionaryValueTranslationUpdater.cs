using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Update;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Update;
public static class AttributeDictionaryValueTranslationUpdater
{
    public static int ApplyUpdates(
        AttributeDictionaryValueTranslation attribute,
        AttributeDictionaryValueTranslationUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.TranslatableId.HasValue)
        {
            attribute.TranslatableId = request.TranslatableId.Value;
            countUpdated++;
        }

        if (request.Language is not null)
        {
            attribute.Language = request.Language;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            attribute.Name = request.Name;
            countUpdated++;
        }

        return countUpdated;
    }
}