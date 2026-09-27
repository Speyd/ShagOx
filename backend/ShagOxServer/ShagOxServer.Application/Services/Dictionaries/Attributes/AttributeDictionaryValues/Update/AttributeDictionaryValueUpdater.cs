using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Update;
public static class AttributeDictionaryValueUpdater
{
    public static int ApplyUpdates(
        AttributeDictionaryValue value,
        AttributeDictionaryValueUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.DictionaryId.HasValue)
        {
            value.DictionaryId = request.DictionaryId.Value;
            countUpdated++;
        }

        if (request.Code is not null)
        {
            value.Code = request.Code;
            countUpdated++;
        }

        if (request.Value is not null)
        {
            value.Value = request.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}