using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Create;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Advertisements.Statuses.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Create;
public class StatusCreateService 
    : IStatusCreateService
{
    private readonly IRepository<Status> _statusRepository;
    private readonly StatusValidator _statusValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<StatusCreateService> _logger;


    public StatusCreateService(
        IRepository<Status> statusRepository,
        StatusValidator statusValidator,
        IUnitOfWork unitOfWork,
        ILogger<StatusCreateService> logger)
    {
        _statusRepository = statusRepository;
        _statusValidator = statusValidator;
        _logger = logger;

        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        StatusCreateRequest request)
    {
        var codeValidation = await _statusValidator
            .NotExistsByCodeAsync(request.Code);

        if (!codeValidation.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(codeValidation.Error);
        }


        var status = StatusCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _statusRepository.Add(status);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create status. Code: {Code}",
                request.Code);

            return Result<CreateResponse>
                    .Fail(EntityErrorResources.AdvertStatusCreateFailed);
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                status.Id,
                DateTime.UtcNow
        ));
    }
}