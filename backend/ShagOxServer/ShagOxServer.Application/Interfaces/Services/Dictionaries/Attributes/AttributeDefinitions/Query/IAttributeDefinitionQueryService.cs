using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
public interface IAttributeDefinitionQueryService
    : IQueryService<AttributeDefinitionDto,
        AttributeDefinition,
        AttributeDefinitionSearchFilter>
{
    Task<Result<AttributeDefinitionDto>> GetByKeyAsync(
        string key);
}