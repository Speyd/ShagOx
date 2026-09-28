using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Mappers;
public static class AttributeDictionaryMapper
{
    public static AttributeDictionaryDto ToDto(
        AttributeDictionary dictionary)
    {
        return new AttributeDictionaryDto(
            dictionary.Id,
            dictionary.Code,
            dictionary.Attributes
                .Select(x => x.Id)
                .ToList(),
            dictionary.Values
                .Select(x => x.Id)
                .ToList()
        );
    }
}