using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Query;
public interface IAttributeDefinitionQueryService
    : IQueryService<AttributeDefinitionDto>
{
    Task<Result<PagedResult<AttributeDefinitionDto>>> Search(
       AttributeDefinitionSearchFilter filter,
       PaginationParams pagination);
}