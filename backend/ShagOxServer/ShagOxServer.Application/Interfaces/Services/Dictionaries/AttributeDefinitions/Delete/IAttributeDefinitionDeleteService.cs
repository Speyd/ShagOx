using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Delete;
public interface IAttributeDefinitionDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
       int id);
}