using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update;
public static class AttributeDefinitionUpdater
{
    public static int ApplyUpdates(
        AttributeDefinition attribute,
        AttributeDefinitionUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.CategoryId.HasValue)
        {
            attribute.CategoryId = request.CategoryId.Value;
            countUpdated++;
        }

        if (request.Key is not null)
        {
            attribute.Key = request.Key;
            countUpdated++;
        }

        if (request.Type.HasValue)
        {
            attribute.Type = request.Type.Value;
            countUpdated++;
        }

        if (request.Required.HasValue)
        {
            attribute.Required = request.Required.Value;
            countUpdated++;
        }

        if (request.Min.HasValue)
        {
            attribute.Min = request.Min.Value;
            countUpdated++;
        }

        if (request.Max.HasValue)
        {
            attribute.Max = request.Max.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}