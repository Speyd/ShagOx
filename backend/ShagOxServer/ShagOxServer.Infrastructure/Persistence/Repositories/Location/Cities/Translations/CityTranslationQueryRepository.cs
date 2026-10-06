using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations;
public class CityTranslationQueryRepository
    : QueryTranslationRepository<City,
        CityTranslation,
        CityTranslationSearchFilter>,
      ICityTranslationQueryRepository
{
    public CityTranslationQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    protected override IQueryable<CityTranslation> ApplyIncludes(
         IQueryable<CityTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<CityTranslation> ApplyFilter(
       IQueryable<CityTranslation> query,
       CityTranslationSearchFilter filter)
    {
        return query.Filter(filter);
    }

    protected override IQueryable<CityTranslation> ApplyIdentificatorFilter(
        IQueryable<CityTranslation> query,
        string identificator)
    {
        return query.Where(x => x.Translatable.Code == identificator);
    }
}