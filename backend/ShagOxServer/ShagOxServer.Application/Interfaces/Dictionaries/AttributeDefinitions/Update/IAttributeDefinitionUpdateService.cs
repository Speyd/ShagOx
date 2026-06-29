using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;

namespace ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Update;
public interface IAttributeDefinitionUpdateService
{
    Task<Result<AttributeDefinitionUpdateResponse>> UpdateAttributeDefinitionAsync(
        int attributeId,
        AttributeDefinitionUpdateRequest request);
}
