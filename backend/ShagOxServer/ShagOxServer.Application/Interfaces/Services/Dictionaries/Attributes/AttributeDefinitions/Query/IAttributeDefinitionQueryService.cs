using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
public interface IAttributeDefinitionQueryService
    : IQueryService<AttributeDefinitionDto, 
        AttributeDefinitionSearchFilter>
{
    Task<Result<AttributeDefinitionDto>> GetByKeyAsync(
        string key);
}