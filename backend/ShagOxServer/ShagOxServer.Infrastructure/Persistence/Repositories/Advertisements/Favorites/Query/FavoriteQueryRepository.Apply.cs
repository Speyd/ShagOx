using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Query;
public partial class FavoriteQueryRepository
    : QueryRepository<Favorite, FavoriteSearchFilter>,
      IFavoriteQueryRepository
{
    protected override IQueryable<Favorite> ApplyIncludes(
         IQueryable<Favorite> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<Favorite> ApplyFilter(
       IQueryable<Favorite> query,
       FavoriteSearchFilter filter)
    {
        return query.Filter(filter);
    }
}