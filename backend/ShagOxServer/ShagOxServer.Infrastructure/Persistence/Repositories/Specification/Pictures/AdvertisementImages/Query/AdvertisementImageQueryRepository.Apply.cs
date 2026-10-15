using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Query;
public partial class AdvertisementImageQueryRepository
    : SearchRepository<AdvertisementImage, ImageSearchFilter>,
      IAdvertisementImageQueryRepository
{
    protected override IQueryable<AdvertisementImage> ApplyIncludes(
        IQueryable<AdvertisementImage> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<AdvertisementImage> ApplyFilter(
        IQueryable<AdvertisementImage> query,
        ImageSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
