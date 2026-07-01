using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Query;
public interface IAttributeDefinitionQueryService
{
    Task<Result<AttributeDefinitionDto>> GetByIdAsync(int id);

    Task<Result<List<AttributeDefinitionDto>>> GetByCategoryAsync(
        int attributeId);

    Task<Result<List<AttributeDefinitionDto>>> SearchByKey(
       string key,
       int page,
       int pageSize);
}
