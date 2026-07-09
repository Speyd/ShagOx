using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Create;
public interface IAttributeDefinitionCreateService
{
    Task<Result<AttributeDefinitionCreateResponse>> CreateAttributeDefinitionAsync(
        AttributeDefinitionCreateRequest request);
}
