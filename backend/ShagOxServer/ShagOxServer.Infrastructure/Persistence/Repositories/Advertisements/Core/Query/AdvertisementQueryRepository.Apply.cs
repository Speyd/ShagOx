using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Query;

public partial class AdvertisementQueryRepository
    : QueryRepository<Advertisement, AdvertisementSearchFilter>,
      IAdvertisementQueryRepository
{
    protected override IQueryable<Advertisement> ApplyIncludes(
        IQueryable<Advertisement> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<Advertisement> ApplyFilter(
        IQueryable<Advertisement> query,
        AdvertisementSearchFilter filter)
    {
        return query.Filter(filter);
    }
}