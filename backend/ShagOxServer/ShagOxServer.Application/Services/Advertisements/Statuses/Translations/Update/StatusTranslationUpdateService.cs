using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Update;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Update;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Validator;
using ShagOxServer.Application.Services.Advertisements.Statuses.Validator;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Update;
public class StatusTranslationUpdateService
    : IStatusTranslationUpdateService
{
    private readonly IRepository<StatusTranslation> _statusRepository;
    private readonly StatusTranslationValidator _statusTranslationValidator;
    private readonly StatusValidator _statusValidator;


    private readonly IUnitOfWork _unitOfWork;


    public StatusTranslationUpdateService(
        IRepository<StatusTranslation> statusRepository,
        StatusTranslationValidator statusTranslationValidator,
        StatusValidator statusValidator,
        IUnitOfWork unitOfWork)
    {
        _statusRepository = statusRepository;
        _statusTranslationValidator = statusTranslationValidator;
        _statusValidator = statusValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int statusTranslationId,
        StatusTranslationUpdateRequest request)
    {
        var status = await _statusTranslationValidator
            .GetByIdAsync(statusTranslationId);

        if (!status.IsSuccess)
            return Result<UpdateResponse>.Fail(status.Error);


        var validation = await
             ValidateUpdatesAsync(status.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = StatusTranslationUpdater
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
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        StatusTranslation status,
        StatusTranslationUpdateRequest request)
    {
        if (request.StatusId is not null &&
           request.StatusId != status.StatusId)
        {
            var validator = await _statusValidator
                .ExistsByIdAsync(request.StatusId.Value);

            if (!validator.IsSuccess)
                return Result<bool>.Fail(validator.Error);
        }

        if (request.Language is not null)
        {
            if ((request.StatusId is not null && 
                request.StatusId != status.StatusId) ||
                request.Language != status.Language)
            {
                var validator = await _statusTranslationValidator
                    .ExistsAsync(
                        request.StatusId ?? status.StatusId, 
                        request.Language);

                if (!validator.IsSuccess)
                    return Result<bool>.Fail(validator.Error);
            }
        }

        return Result<bool>.Success(true);
    }
}