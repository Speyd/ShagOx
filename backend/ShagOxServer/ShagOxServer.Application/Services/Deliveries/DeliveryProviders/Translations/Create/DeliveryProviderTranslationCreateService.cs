using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Validator;
using ShagOxServer.Domain.Entities.Deliveries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Create;
public class DeliveryProviderTranslationCreateService
    : IDeliveryProviderTranslationCreateService
{
    private readonly IRepository<DeliveryProviderTranslation> _deliveryRepository;
    private readonly DeliveryProviderTranslationValidator _deliveryValidator;

    //private readonly DeliveryProviderTranslationInvalidationService _trnsInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeliveryProviderTranslationCreateService> _logger;


    public DeliveryProviderTranslationCreateService(
        IRepository<DeliveryProviderTranslation> deliveryRepository,
        DeliveryProviderTranslationValidator deliveryValidator,
        //StatusTranslationInvalidationService trnsInvalid,
        IUnitOfWork unitOfWork,
        ILogger<DeliveryProviderTranslationCreateService> logger)
    {
        _deliveryRepository = deliveryRepository;
        _deliveryValidator = deliveryValidator;
        //_trnsInvalid = trnsInvalid;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        DeliveryProviderTranslationCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var codeValidation = await _deliveryValidator
            .NotExistsAsync(request.TranslatableId,
                request.Language, cancellationToken);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var delivery = DeliveryProviderTranslationCreater
            .Create(request);

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
                "Failed to create delivery provider translation. " +
                "TranslatableId: {TranslatableId}",
                request.TranslatableId);

            return Result<CreateResponse>.Fail(
                EntityErrorResources.DeliveryProviderTranslationCreateFailed);
        }

        //await _trnsInvalid.InvalidateCreateAsync(
        //    BaseTranslationCacheMapper.ToInfo(status));

        return Result<CreateResponse>.Success(
            new CreateResponse(
                delivery.Id,
                DateTime.UtcNow
        ));
    }
}
