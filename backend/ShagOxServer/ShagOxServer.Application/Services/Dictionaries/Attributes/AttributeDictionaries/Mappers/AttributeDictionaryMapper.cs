using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Mappers;
public static class AttributeDictionaryMapper
{
    public static AttributeDictionaryDto ToDto(
        AttributeDictionary attributeDict)
    {
        return new AttributeDictionaryDto(
            attributeDict.Id,
            attributeDict.Code,
            attributeDict.Attributes
                .Select(x => x.Id)
                .ToList(),
            attributeDict.Values
                .Select(x => x.Id)
                .ToList()
        );
    }
}