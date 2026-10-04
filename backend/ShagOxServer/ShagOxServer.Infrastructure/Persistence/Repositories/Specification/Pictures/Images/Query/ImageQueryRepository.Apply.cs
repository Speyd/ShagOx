using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Query;
public partial class ImageQueryRepository
    : QueryRepository<Image, ImageSearchFilter>,
      IImageQueryRepository
{
    protected override IQueryable<Image> ApplyIncludes(
        IQueryable<Image> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<Image> ApplyFilter(
        IQueryable<Image> query,
        ImageSearchFilter filter)
    {
        return query.Filter(filter);
    }
}