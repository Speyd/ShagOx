using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames.Extensions;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Validator;
public class AdvertisementValidator
    : BaseValidator<Advertisement>
{
    private readonly IAdvertisementQueryRepository _advertisementQueryRepository;
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
        _advertisementQueryRepository = advertisementQueryRepository;
    }

    public async Task<Result<Advertisement>> GetByIdShortIncludeAsync(
        long entityId,
        CancellationToken cancellationToken = default,
        EntityStatus findStatus = EntityStatus.ActiveAndHidden)
    {
        var entity = await _advertisementQueryRepository
            .GetByIdShortIncludeAsync(entityId, cancellationToken, findStatus);

        if (entity is null)
        {
            return Result<Advertisement>.NotFound(
                EntityNameExtensions.GetLocalizedName<Advertisement>());
        }

        return Result<Advertisement>.Success(entity);
    }
}
