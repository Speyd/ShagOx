using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Domain.Filters.Advertisements.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Query;
public interface IStatusTranslationQueryService
    : IQueryService<StatusTranslationDto>
{
    Task<Result<PagedResult<StatusTranslationDto>>> Search(
       StatusTranslationSearchFilter filter,
       PaginationParams pagination);
}