using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Update;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Advertisements.Statuses.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Update;
public class StatusUpdateService
    : IStatusUpdateService
{
    private readonly IRepository<Status> _statusRepository;
    private readonly StatusValidator _statusValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<StatusUpdateService> _logger;


    public StatusUpdateService(
        IRepository<Status> statusRepository,
        StatusValidator statusValidator,
        IUnitOfWork unitOfWork,
        ILogger<StatusUpdateService> logger)
    {
        _statusRepository = statusRepository;
        _statusValidator = statusValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int statusId,
        StatusUpdateRequest request)
    {
        var status = await _statusValidator.GetByIdAsync(statusId);
        if (!status.IsSuccess)
            return Result<UpdateResponse>.Fail(status.Error);


        var validation = await
             ValidateUpdatesAsync(status.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = StatusUpdater
            .ApplyUpdates(status.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _statusRepository.Update(status.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update status. Id: {Id}",
                statusId);

            return Result<UpdateResponse>
                     .Fail(EntityErrorResources.AdvertStatusUpdateFailed);
        }

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        Status status,
        StatusUpdateRequest request)
    {
        if (request.Code is not null &&
           request.Code != status.Code)
        {
            var validator = await _statusValidator
                .ExistsByCodeAsync(request.Code);

            if (!validator.IsSuccess)
                return Result<bool>.Fail(validator.Error);
        }

        return Result<bool>.Success(true);
    }
}