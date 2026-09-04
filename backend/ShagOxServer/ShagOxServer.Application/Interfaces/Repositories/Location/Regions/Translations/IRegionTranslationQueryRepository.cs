using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Advertisements.Translations;
using ShagOxServer.Domain.Filters.Location.Regions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
public interface IRegionTranslationQueryRepository
    : IQueryTranslationsRepository<RegionTranslation>
{
    Task<PagedResult<RegionTranslation>> Search(
       RegionTranslationSearchFilter filter,
       PaginationParams pagination);
}