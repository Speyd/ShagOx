using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.CategoryImages;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.CategoryImages;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.CategoryImages.Query;
public partial class CategoryImageQueryRepository
    : PictureQueryRepository<CategoryImage, 
        CategoryImageSearchFilter>,
      ICategoryImageQueryRepository
{
    public CategoryImageQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }
}
