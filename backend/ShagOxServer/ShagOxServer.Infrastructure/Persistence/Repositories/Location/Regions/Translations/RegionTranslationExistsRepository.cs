using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations;
public class RegionTranslationExistsRepository
    : ExistsTranslationRepository<Region,
        RegionTranslation>,
      IRegionTranslationExistsRepository
{
    public RegionTranslationExistsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    protected override IQueryable<RegionTranslation> ApplyIncludes(
         IQueryable<RegionTranslation> query,
        EntityStatus? status = null)
    {
        return query.WithIncludes(status);
    }

    protected override IQueryable<RegionTranslation> ApplyIdentificatorFilter(
        IQueryable<RegionTranslation> query,
        string identificator)
    {
        return query.Where(x => x.Translatable.Code == identificator);
    }
}
