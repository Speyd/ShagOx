using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
public static class AttributeDictionaryValueCreater
{
    public static AttributeDictionaryValue Create(
       AttributeDictionaryValueCreateRequest request)
    {
        return new AttributeDictionaryValue
        {
            DictionaryId = request.DictionaryId,
            Code = request.Code,
            Value = request.Value,
        };
    }
}