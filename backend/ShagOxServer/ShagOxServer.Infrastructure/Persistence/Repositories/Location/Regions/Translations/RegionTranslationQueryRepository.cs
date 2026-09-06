using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Regions.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations;
public class RegionTranslationQueryRepository
    : QueryTranslationRepository<RegionTranslation>,
      IRegionTranslationQueryRepository
{
    public RegionTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<RegionTranslation>> Search(
        RegionTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.RegionTranslations
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}