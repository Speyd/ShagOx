using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Update;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Update;
public static class AttributeDefinitionTranslationUpdater
{
    public static int ApplyUpdates(
        AttributeDefinitionTranslation attribute,
        AttributeDefinitionTranslationUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.AttributeDefenitionId.HasValue)
        {
            attribute.TranslatableId = request.AttributeDefenitionId.Value;
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