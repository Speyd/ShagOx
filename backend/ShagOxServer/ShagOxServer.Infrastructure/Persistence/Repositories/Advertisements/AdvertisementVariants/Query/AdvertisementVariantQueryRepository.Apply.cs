using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Query;

public partial class AdvertisementVariantQueryRepository
    : SearchRepository<AdvertisementVariant, AdvertisementVariantSearchFilter>,
      IAdvertisementVariantQueryRepository
{

    protected override IQueryable<AdvertisementVariant> ApplyIncludes(
        IQueryable<AdvertisementVariant> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<AdvertisementVariant> ApplyFilter(
        IQueryable<AdvertisementVariant> query,
        AdvertisementVariantSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
