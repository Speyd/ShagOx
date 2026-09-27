using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations;
public class CityTranslationQueryRepository
    : QueryTranslationRepository<CityTranslation,
        CityTranslationSearchFilter>,
      ICityTranslationQueryRepository
{
    public CityTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<CityTranslation> ApplyFilter(
       IQueryable<CityTranslation> query,
       CityTranslationSearchFilter filter)
    {
        return query.Filter(filter);
    }
}