using ShagOxServer.Application.DTOs.Location.Regions.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Location.Regions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Query;
public interface IRegionTranslationQueryService
    : IQueryTranslationService<RegionTranslationDto>
{
    Task<Result<PagedResult<RegionTranslationDto>>> Search(
       RegionTranslationSearchFilter filter,
       PaginationParams pagination);
}