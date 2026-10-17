using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Core.Create;
public abstract class PictureCreateService<TOwner, TPicture, TRequest>
    : IPictureCreateService<TPicture, TRequest>,
    IPictureCache<TPicture>
    where TOwner : BaseEntity
    where TPicture : BasePicture<TOwner>
    where TRequest : PictureCreateRequest
{
    protected readonly IRepository<TPicture> _pictureRepository;

    protected readonly PictureValidator _pictureValidator;
    protected readonly BaseValidator<TOwner> _ownerValidator;

    protected readonly IUnitOfWork _unitOfWork;


    protected readonly ILogger<PictureCreateService<TOwner, 
        TPicture, TRequest>> _logger;


    public PictureCreateService(
        IRepository<TPicture> pictureRepository,
        PictureValidator pictureValidator,
        BaseValidator<TOwner> ownerValidator,
        IUnitOfWork unitOfWork,
        ILogger<PictureCreateService<TOwner, 
            TPicture, TRequest>> logger)
    {
        _pictureRepository = pictureRepository;
        _pictureValidator = pictureValidator;
        _ownerValidator = ownerValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    protected abstract TPicture CreatePicture(
        TRequest request,
        PictureLoaderUploadResponse response);

    public abstract Task InvalidateCache(
        TPicture picture);

    public virtual async Task<Result<PictureCreateResponse>> CreateAsync(
        TRequest request)
    {
        var ownerValid = await _ownerValidator
            .ExistsByIdAsync(request.OwnerId);

        if (!ownerValid.IsSuccess)
        {
            return Result<PictureCreateResponse>
                .Fail(ownerValid.Error);
        }

        await _unitOfWork.BeginTransactionAsync();

        var resultLoaderValid = await _pictureValidator
           .PictureUploadValidator(request.File);

        try
        {
            if (!resultLoaderValid.IsSuccess)
                throw new Exception("Fail picture upload");

            var picture = CreatePicture(
                request,
                resultLoaderValid.Value!
            );

            _pictureRepository.Add(picture);

            await _unitOfWork.CommitAsync();

            return await Success(picture);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                $"Failed to create {typeof(TPicture).Name}. " +
                "OwnerId: {OwnerId}",
                request.OwnerId);


            if (resultLoaderValid.Value?.PublicId is not null)
            {
                await _pictureValidator.PictureDeleteValidator(
                    resultLoaderValid.Value.PublicId);
            }

            if (!resultLoaderValid.IsSuccess)
            {
                return Result<PictureCreateResponse>
                    .Fail(resultLoaderValid.Error);
            }

            throw;
        }
    }

    public virtual async Task<Result<PictureCreateResponse>> CreateInternalAsync(
       TRequest request)
    {
        var resultLoaderValid = await _pictureValidator
            .PictureUploadValidator(request.File);

        try
        {
            if (!resultLoaderValid.IsSuccess)
                throw new Exception("Fail picture upload");

            var picture = CreatePicture(
                request,
                resultLoaderValid.Value!
            );

            _pictureRepository.Add(picture);

            await _unitOfWork.SaveChangesAsync();

            return await Success(picture);
        }
        catch (Exception ex)
        {
            _logger.LogError(
               ex,
               $"Failed to create {typeof(TPicture).Name}. " +
               "OwnerId: {OwnerId}",
               request.OwnerId);

            if (resultLoaderValid.Value?.PublicId is not null)
            {
                await _pictureValidator.PictureDeleteValidator(
                    resultLoaderValid.Value.PublicId);
            }

            if (!resultLoaderValid.IsSuccess)
            {
                return Result<PictureCreateResponse>
                    .Fail(resultLoaderValid.Error);
            }

            throw;
        }
    }


    private async Task<Result<PictureCreateResponse>> Success(
        TPicture picture)
    {
        _logger.LogInformation(
            $"{typeof(TPicture).Name} created successfully. " +
            "Id: {Id}, OwnerId: {OwnerId}",
            picture.Id,
            picture.OwnerId);

        await InvalidateCache(picture);

        return Result<PictureCreateResponse>.Success(
            new PictureCreateResponse(
                picture.Id,
                picture.PublicId,
                DateTime.UtcNow
        ));
    }
}
