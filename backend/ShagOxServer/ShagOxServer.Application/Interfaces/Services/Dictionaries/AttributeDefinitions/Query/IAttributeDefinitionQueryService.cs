using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Query;
public interface IAttributeDefinitionQueryService
{
    Task<Result<AttributeDefinitionDto>> GetByIdAsync(int id);

    Task<Result<PagedResult<AttributeDefinitionDto>>> GetPagedAsync(
        PaginationParams pagination);

    Task<Result<PagedResult<AttributeDefinitionDto>>> Search(
       AttributeDefinitionSearchFilter filter,
       PaginationParams pagination);
}