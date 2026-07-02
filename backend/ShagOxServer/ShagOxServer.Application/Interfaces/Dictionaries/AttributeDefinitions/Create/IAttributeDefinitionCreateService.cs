using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.SharedKernel.Results;

namespace ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Create;
public interface IAttributeDefinitionCreateService
{
    Task<Result<AttributeDefinitionCreateResponse>> CreateAttributeDefinitionAsync(
        AttributeDefinitionCreateRequest request);
}
