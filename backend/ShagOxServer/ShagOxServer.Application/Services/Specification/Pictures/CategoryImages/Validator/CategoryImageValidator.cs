using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Validator;
public class CategoryImageValidator
    : BaseValidator<CategoryImage>
{
    public CategoryImageValidator(
        IRepository<CategoryImage> repository,
        IQueryRepository<CategoryImage> queryRepository,
        IExistsRepository<CategoryImage> existsRepository,
        IDataCacheService cache
    )
        : base(repository, queryRepository, existsRepository, cache)
    {
    }
}
