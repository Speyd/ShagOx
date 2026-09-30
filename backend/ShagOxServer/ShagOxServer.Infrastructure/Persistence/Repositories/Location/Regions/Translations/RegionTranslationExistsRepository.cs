using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations;
public class RegionTranslationExistsRepository
    : ExistsTranslationRepository<Region,
        RegionTranslation>,
      IRegionTranslationExistsRepository
{
    public RegionTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }

    protected override IQueryable<RegionTranslation> ApplyIncludes(
         IQueryable<RegionTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<RegionTranslation> ApplyIdentificatorFilter(
        IQueryable<RegionTranslation> query,
        string identificator)
    {
        return query.Where(x => x.Translatable.Code == identificator);
    }
}