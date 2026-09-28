using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Regions.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations;
public class RegionTranslationQueryRepository
    : QueryTranslationRepository<Region,
        RegionTranslation, 
        RegionTranslationSearchFilter>,
      IRegionTranslationQueryRepository
{
    public RegionTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<RegionTranslation> ApplyIncludes(
         IQueryable<RegionTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<RegionTranslation> ApplyFilter(
       IQueryable<RegionTranslation> query,
       RegionTranslationSearchFilter filter)
    {
        return query.Filter(filter);
    }
}