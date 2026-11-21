using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Update;
using ShagOxServer.Domain.Entities.Deliveries.Translations;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Update;
public static class DeliveryProviderTranslationUpdater
{
    public static int ApplyUpdates(
        DeliveryProviderTranslation status,
        DeliveryProviderTranslationUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.TranslatableId.HasValue)
        {
            status.TranslatableId = request.TranslatableId.Value;
            countUpdated++;
        }

        if (request.Language is not null)
        {
            status.Language = request.Language;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            status.Name = request.Name;
            countUpdated++;
        }

        return countUpdated;
    }
}
