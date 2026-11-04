using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;
public class AdvertisementImageValidator
    : BaseValidator<AdvertisementImage>
{
    public AdvertisementImageValidator(
        IRepository<AdvertisementImage> repository,
        IQueryRepository<AdvertisementImage> imageRepository,
        IExistsRepository<AdvertisementImage> imageExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, imageRepository, imageExistsRepository, cache)
    {
    }
}
