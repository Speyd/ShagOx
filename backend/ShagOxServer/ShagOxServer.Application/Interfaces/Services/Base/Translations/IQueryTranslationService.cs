using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base.Translations;
public interface IQueryTranslationService<TDto>
    : IQueryService<TDto>
{
    Task<Result<PagedResult<TDto>>> GetPagedAsync(
       PaginationParams pagination,
       string language);
}