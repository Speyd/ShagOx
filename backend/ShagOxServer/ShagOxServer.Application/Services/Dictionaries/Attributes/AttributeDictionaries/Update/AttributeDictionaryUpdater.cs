using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries.Update;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Update;
public static class AttributeDictionaryUpdater
{
    public static int ApplyUpdates(
        AttributeDictionary dictionary,
        AttributeDictionaryUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Code is not null)
        {
            dictionary.Code = request.Code;
            countUpdated++;
        }

        return countUpdated;
    }
}