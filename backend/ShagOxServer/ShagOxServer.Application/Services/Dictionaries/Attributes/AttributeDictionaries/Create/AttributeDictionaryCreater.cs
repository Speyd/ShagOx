using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries.Create;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Create;
public static class AttributeDictionaryCreater
{
    public static AttributeDictionary Create(
       AttributeDictionaryCreateRequest request)
    {
        return new AttributeDictionary
        {
            Code = request.Code,      
        };
    }
}