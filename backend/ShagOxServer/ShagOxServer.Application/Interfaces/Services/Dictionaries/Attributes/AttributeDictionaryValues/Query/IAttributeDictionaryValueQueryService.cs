using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
public interface IAttributeDictionaryValueQueryService
    : IQueryService<AttributeDictionaryValueDto,
        AttributeDictionaryValue,
        AttributeDictionaryValueSearchFilter>
{
    Task<Result<PagedResult<AttributeDictionaryValueDto>>> 
        GetByDictionaryAsync(
        long dictionaryId,
        PaginationParams pagination);
}