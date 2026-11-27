using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Query;
public partial class FavoriteQueryRepository
    : SearchRepository<Favorite, FavoriteSearchFilter>,
      IFavoriteQueryRepository
{
    protected override IQueryable<Favorite> ApplyIncludes(
         IQueryable<Favorite> query,
        EntityStatus? status = null)
    {
        return query.WithIncludes();
    }

    public override IQueryable<Favorite> ApplyFilter(
       IQueryable<Favorite> query,
       FavoriteSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
