using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.SharedKernel.Results;

namespace ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Delete;
public interface IAttributeDefinitionDeleteService
{
    Task<Result<AttributeDefinitionDeleteResponse>> DeleteAttributeDefinitionAsync(
       int id);
}
