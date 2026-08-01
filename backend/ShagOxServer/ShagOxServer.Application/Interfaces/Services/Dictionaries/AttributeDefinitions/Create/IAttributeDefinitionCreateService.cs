using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Create;
public interface IAttributeDefinitionCreateService
{
    Task<Result<CreateResponse>> CreateAsync(
        AttributeDefinitionCreateRequest request);
}