using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Update;
public interface IAttributeDefinitionUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
        int attributeId,
        AttributeDefinitionUpdateRequest request);
}