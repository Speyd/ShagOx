using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;
using ShagOxServer.SharedKernel.Results;

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
