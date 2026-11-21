using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.CategoryImages;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.CategoryImages;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.CategoryImages.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.CategoryImages.Query;
public partial class CategoryImageQueryRepository
    : PictureQueryRepository<CategoryImage, CategoryImageSearchFilter>,
      ICategoryImageQueryRepository
{
    protected override IQueryable<CategoryImage> ApplyIncludes(
        IQueryable<CategoryImage> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<CategoryImage> ApplyFilter(
        IQueryable<CategoryImage> query,
        CategoryImageSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
