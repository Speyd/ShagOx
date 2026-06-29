using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Delete;

namespace ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Delete;
public interface IAttributeDefinitionDeleteService
{
    Task<Result<AttributeDefinitionDeleteResponse>> DeleteAttributeDefinitionAsync(
       int id);
}
