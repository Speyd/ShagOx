using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Create;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Create;
public static class AttributeDefinitionCreater
{
    public static AttributeDefinition Create(
       AttributeDefinitionCreateRequest request)
    {
        return new AttributeDefinition
        {
            CategoryId = request.CategoryId,
            Key = request.Key,
            Type = request.Type,
            Required = request.Required,
            Min = request.Min,
            Max = request.Max,
        };
    }
}