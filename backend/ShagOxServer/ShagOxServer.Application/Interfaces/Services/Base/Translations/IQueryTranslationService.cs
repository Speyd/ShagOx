using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base.Translations;
public interface IQueryTranslationService<TDto, TFilter>
    : IQueryService<TDto, TFilter>
    where TDto : BaseDto
    where TFilter : BaseFilter

{
    Task<Result<PagedResult<TDto>>> GetPagedAsync(
       PaginationParams pagination,
       string language);
}