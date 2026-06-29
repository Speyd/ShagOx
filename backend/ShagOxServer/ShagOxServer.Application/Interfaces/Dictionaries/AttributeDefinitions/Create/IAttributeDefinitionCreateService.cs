using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;

namespace ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Create;
public interface IAttributeDefinitionCreateService
{
    Task<Result<AttributeDefinitionCreateResponse>> CreateAttributeDefinitionAsync(
        AttributeDefinitionCreateRequest request);
}
