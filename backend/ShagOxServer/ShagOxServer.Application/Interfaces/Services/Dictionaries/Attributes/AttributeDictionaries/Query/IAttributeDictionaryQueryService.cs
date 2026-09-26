using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Query;
public interface IAttributeDictionaryQueryService
    : IQueryService<AttributeDictionaryDto>
{
    Task<Result<PagedResult<AttributeDictionaryDto>>> Search(
       AttributeDictionarySearchFilter filter,
       PaginationParams pagination);
}