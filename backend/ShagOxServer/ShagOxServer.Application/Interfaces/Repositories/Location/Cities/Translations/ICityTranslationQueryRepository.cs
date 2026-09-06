using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
public interface ICityTranslationQueryRepository
    : IQueryTranslationRepository<CityTranslation>
{
    Task<PagedResult<CityTranslation>> Search(
       CityTranslationSearchFilter filter,
       PaginationParams pagination);
}