using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations;
public class CityTranslationQueryRepository
    : QueryTranslationRepository<CityTranslation>,
      ICityTranslationQueryRepository
{
    public CityTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<CityTranslation>> Search(
        CityTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.CityTranslations
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}