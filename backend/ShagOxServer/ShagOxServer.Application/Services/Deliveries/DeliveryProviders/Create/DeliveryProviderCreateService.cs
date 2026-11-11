using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Create;
public class DeliveryProviderCreateService
    : IDeliveryProviderCreateService
{
    private readonly IRepository<DeliveryProvider> _statusRepository;
    private readonly DeliveryProviderValidator _statusValidator;

    //private readonly StatusInvalidationService _statusInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeliveryProviderCreateService> _logger;


    public DeliveryProviderCreateService(
        IRepository<DeliveryProvider> statusRepository,
        DeliveryProviderValidator statusValidator,
        //StatusInvalidationService statusInvalid,
        IUnitOfWork unitOfWork,
        ILogger<DeliveryProviderCreateService> logger)
    {
        _statusRepository = statusRepository;
        _statusValidator = statusValidator;
        //_statusInvalid = statusInvalid;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        DeliveryProviderCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var codeValidation = await _statusValidator
            .NotExistsByCodeAsync(request.Code, cancellationToken);

        if (!codeValidation.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(codeValidation.Error);
        }


        var status = DeliveryProviderCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _statusRepository.Add(status);

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

        //await _statusInvalid.InvalidateCreateAsync(
        //    StatusCacheMapper.ToInfo(status));

        return Result<CreateResponse>.Success(
            new CreateResponse(
                status.Id,
                DateTime.UtcNow
        ));
    }
}
