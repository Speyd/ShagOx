using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;
using Twilio.Http;

namespace ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
public abstract class PictureUpdateService<TOwner, TPicture, TRequest>
    : IPictureUpdateService<TPicture, TRequest>,
    IPictureCache<TPicture>
    where TOwner : BaseEntity
    where TPicture : BasePicture<TOwner>
    where TRequest : PictureUpdateRequest
{
    private readonly IRepository<TPicture> _pictureRepository;

    protected readonly PictureValidator _pictureLoadValidator;

    private readonly BaseValidator<TPicture> _pictureValidator;
    private readonly BaseValidator<TOwner> _ownerValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PictureUpdateService<TOwner, 
        TPicture, TRequest>> _logger;


    public PictureUpdateService(
        IRepository<TPicture> pictureRepository,
        PictureValidator pictureLoadValidator,
        BaseValidator<TPicture> pictureValidator,
        BaseValidator<TOwner> ownerValidator,
        IUnitOfWork unitOfWork,
        ILogger<PictureUpdateService<TOwner, 
            TPicture, TRequest>> logger
    )
    {
        _pictureRepository = pictureRepository;
        _pictureLoadValidator = pictureLoadValidator;
        _pictureValidator = pictureValidator;
        _ownerValidator = ownerValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public abstract Task InvalidateCache(
       TPicture picture);

    protected abstract int ApplyUpdates(
        TPicture picture,
        TRequest request,
        PictureLoaderUploadResponse? response);

    protected async Task<Result<bool>> ValidateOwnerAsync(
       TPicture picture,
       TRequest request)
    {
        if (request.OwnerId is null ||
            picture.OwnerId == request.OwnerId)
        {
            return Result<bool>.Success(true);
        }

        var existsValidator = await _ownerValidator
            .ExistsByIdAsync(request.OwnerId.Value);

        return existsValidator;
    }

    public virtual async Task<Result<PictureUpdateResponse>> UpdateAsync(
        long pictureId,
        TRequest request)
    {
        var picture = await _pictureValidator
            .GetByIdAsync(pictureId);

        if (!picture.IsSuccess)
        {
            return Result<PictureUpdateResponse>
                .Fail(picture.Error);
        }

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var result = await UpdateInternalAsync(
                picture.Value!,
                request);

            if (!result.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();
                return result;
            }

            await _unitOfWork.CommitAsync();

            await InvalidateCache(picture.Value!);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogWarning(
                ex,
                $"Concurrency conflict while updating {typeof(TPicture).Name}." +
                " Id: {Id}",
                pictureId);

            return Result<PictureUpdateResponse>.Fail(
                EntityErrorResources.ConcurrencyConflictMessage);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                $"Failed to update {typeof(TPicture).Name}. " + 
                "Id: {Id}",
                pictureId);

            throw;
        }
    }


    public virtual async Task<Result<PictureUpdateResponse>> UpdateInternalAsync(
        TPicture picture,
        TRequest request)
    {
        var oldPictureId = picture.PublicId;
        var oldPictureUrl = picture.PublicId;

        PictureLoaderUploadResponse? responseLoad = null;
        if (request.File is not null)
        {

            var responseLoadRes = await _pictureLoadValidator
                .PictureUploadValidator(request.File);

            if (!responseLoadRes.IsSuccess)
            {
                Result<PictureUpdateResponse>
                    .Fail(responseLoadRes.Error);
            }

            responseLoad = responseLoadRes.Value;
        }

        var updatedCount = ApplyUpdates(
            picture,
            request,
            responseLoad
        );

        var response = new PictureUpdateResponse(
            updatedCount,
            oldPictureId,
            oldPictureUrl,
            responseLoad?.PublicId,
            responseLoad?.Url,
            DateTime.UtcNow);

        if (updatedCount == 0)
        {
            return Result<PictureUpdateResponse>.Success(response);
        }

        _pictureRepository.Update(picture);

        _logger.LogInformation(
            $"{typeof(TPicture).Name} updated successfully. " + 
            "Id: {Id}",
            picture.Id);

        return Result<PictureUpdateResponse>.Success(response);
    }

    public async Task RollbackAsync(
        TPicture picture,
        PictureUpdateResponse response)
    {
        if (response.NewPublicId is null)
            return;

        if(response.OldPublicId is null)
            return;

        picture.PublicId = response.OldPublicId;
        picture.Url = response.OldUrl ?? "";


        var responseLoadRes = await _pictureLoadValidator
            .PictureDeleteValidator(response.NewPublicId);
    }
}
