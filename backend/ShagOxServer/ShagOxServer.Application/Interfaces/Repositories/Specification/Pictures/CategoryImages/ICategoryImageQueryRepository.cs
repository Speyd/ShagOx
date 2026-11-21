using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Single;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.CategoryImages;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.CategoryImages;
public interface ICategoryImageQueryRepository
    : ISearchRepository<CategoryImage, CategoryImageSearchFilter>,
    IPictureQueryRepository<CategoryImage>
{
}
