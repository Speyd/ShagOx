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

        if (request.CategoryId is not null)
        {
            attribute.CategoryId = request.CategoryId.Value;
            countUpdated++;
        }

        if (request.Key is not null)
        {
            attribute.Key = request.Key;
            countUpdated++;
        }

        if (request.Type is not null)
        {
            attribute.Type = request.Type.Value;
            countUpdated++;
        }

        if (request.Required is not null)
        {
            attribute.Required = request.Required.Value;
            countUpdated++;
        }

        if (request.Min is not null)
        {
            attribute.Min = request.Min.Value;
            countUpdated++;
        }

        if (request.Max is not null)
        {
            attribute.Max = request.Max.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}