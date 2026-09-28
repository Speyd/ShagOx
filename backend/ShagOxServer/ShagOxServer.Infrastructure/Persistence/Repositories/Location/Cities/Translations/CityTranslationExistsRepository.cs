using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations;
public class CityTranslationExistsRepository
    : ExistsTranslationRepository<City, CityTranslation>,
      ICityTranslationExistsRepository
{
    public CityTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<CityTranslation> ApplyIncludes(
         IQueryable<CityTranslation> query)
    {
        return query.WithIncludes();
    }
}