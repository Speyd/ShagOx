using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;
public class DeliveryProviderValidator
    : BaseValidator<DeliveryProvider>
{
    private readonly IDeliveryProviderExistsRepository _deliveryExistsRepository;


    public DeliveryProviderValidator(
        IRepository<DeliveryProvider> repository,
        IQueryRepository<DeliveryProvider> statusRepository,
        IDeliveryProviderExistsRepository deliveryExistsRepository,
        IDataCacheService cache
    )
        : base(repository, statusRepository, deliveryExistsRepository, cache)
    {
        _deliveryExistsRepository = deliveryExistsRepository;
    }


    public async Task<Result<bool>> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        if (!await _deliveryExistsRepository
            .ExistsByCodeAsync(code, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Status);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        if (await _deliveryExistsRepository
            .ExistsByCodeAsync(code, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.Status);
        }

        return Result<bool>.Success(true);
    }
}
