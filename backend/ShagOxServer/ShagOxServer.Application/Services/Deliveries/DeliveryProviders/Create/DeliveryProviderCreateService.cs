using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Mapping;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Create;
public class DeliveryProviderCreateService
    : IDeliveryProviderCreateService
{
    private readonly IRepository<DeliveryProvider> _deliveryRepository;
    private readonly DeliveryProviderValidator _deliveryValidator;

    private readonly DeliveryProviderInvalidationService _deliveryInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeliveryProviderCreateService> _logger;


    public DeliveryProviderCreateService(
        IRepository<DeliveryProvider> deliveryRepository,
        DeliveryProviderValidator deliveryValidator,
        DeliveryProviderInvalidationService deliveryInvalid,
        IUnitOfWork unitOfWork,
        ILogger<DeliveryProviderCreateService> logger)
    {
        _deliveryRepository = deliveryRepository;
        _deliveryValidator = deliveryValidator;
        _deliveryInvalid = deliveryInvalid;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        DeliveryProviderCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var codeValidation = await _deliveryValidator
            .NotExistsByCodeAsync(request.Code, cancellationToken);

        if (!codeValidation.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(codeValidation.Error);
        }


        var delivery = DeliveryProviderCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _deliveryRepository.Add(delivery);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create delivery provider. Code: {Code}",
                request.Code);

            return Result<CreateResponse>
                    .Fail(EntityErrorResources.DeliveryProviderCreateFailed);
        }

        await _deliveryInvalid.InvalidateCreateAsync(
            DeliveryProviderCacheMapper.ToInfo(delivery));

        return Result<CreateResponse>.Success(
            new CreateResponse(
                delivery.Id,
                DateTime.UtcNow
        ));
    }
}
