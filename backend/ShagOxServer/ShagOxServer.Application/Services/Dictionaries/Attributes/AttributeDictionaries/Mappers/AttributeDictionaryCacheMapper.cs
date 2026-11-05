using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Mappers;
public static class AttributeDictionaryCacheMapper
{
    public static AttributeDictionaryCacheInfo ToInfo(
        AttributeDictionary dictionary,
        EntityStatus status = EntityStatus.Active)
    {
        return new AttributeDictionaryCacheInfo(
            dictionary.Id,
            status
        );
    }
}
