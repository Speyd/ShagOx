using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;

namespace ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Query;
public interface IAttributeDefinitionQueryService
{
    Task<Result<AttributeDefinitionDto>> GetByIdAsync(int id);

    Task<Result<List<AttributeDefinitionDto>>> GetByCategoryAsync(
        int attributeId);

}
