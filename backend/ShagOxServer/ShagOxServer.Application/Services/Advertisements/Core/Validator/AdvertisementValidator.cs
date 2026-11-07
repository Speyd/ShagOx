using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Validator;
public class AdvertisementValidator
    : BaseValidator<Advertisement>
{
    public AdvertisementValidator(
        IRepository<Advertisement> repository,
        IAdvertisementQueryRepository advertisementQueryRepository,
        IAdvertisementExistsRepository advertisementExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, advertisementQueryRepository, 
            advertisementExistsRepository,
            cache)
    {
    }
}
