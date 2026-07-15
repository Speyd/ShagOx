using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Update;
public interface IAttributeDefinitionUpdateService
{
    Task<Result<AttributeDefinitionUpdateResponse>> UpdateAsync(
        int attributeId,
        AttributeDefinitionUpdateRequest request);
}