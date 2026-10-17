using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Core.Delete;
public abstract class PictureDeleteService<TOwner, TPicture>
    : IPictureDeleteService<TPicture>,
    IPictureCache<TPicture>
    where TOwner : BaseEntity
    where TPicture : BasePicture<TOwner>
{
    private readonly IRepository<TPicture> _pictureRepository;
    private readonly BaseValidator<TPicture> _pictureBaseValidator;
    private readonly PictureValidator _pictureValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PictureDeleteService<TOwner, TPicture>> _logger;


    public PictureDeleteService(
        IRepository<TPicture> pictureRepository,
        BaseValidator<TPicture> pictureBaseValidator,
        PictureValidator pictureValidator,
        IUnitOfWork unitOfWork,
        ILogger<PictureDeleteService<TOwner, TPicture>> logger)
    {
        _pictureRepository = pictureRepository;
        _pictureBaseValidator = pictureBaseValidator;
        _pictureValidator = pictureValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public abstract Task InvalidateCache(
        TPicture picture);

    public virtual async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var picture = await _pictureBaseValidator
            .GetByIdAsync(id);

        if (!picture.IsSuccess)
            return Result<DeleteResponse>.Fail(picture.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var result = await DeleteInternalAsync(picture.Value!);

            if (!result.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();
                return result;
            }

            await _unitOfWork.CommitAsync();

            await InvalidateCache(picture.Value!);

            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
              ex,
              $"Failed to delete {typeof(TPicture).Name}. " +
              "Id: {Id}",
              id);

            throw;
        }
    }

    public virtual async Task<Result<DeleteResponse>> DeleteInternalAsync(
        TPicture picture)
    {
        _pictureRepository.Delete(picture);

        var result = await _pictureValidator
            .PictureDeleteValidator(picture.PublicId);

        if (!result.IsSuccess)
            return Result<DeleteResponse>.Fail(result.Error);

        _logger.LogInformation(
            $"{typeof(TPicture).Name} deleted successfully. " + 
            "Id: {Id}",
            picture.Id);

        return Result<DeleteResponse>.Success(
            new DeleteResponse(
                picture.Id,
                DateTime.UtcNow));
    }

    public virtual async Task<Result<DeleteResponse>> DeleteRecordAsync(
        long id)
    {
        var picture = await _pictureBaseValidator
            .GetByIdAsync(id);

        if (!picture.IsSuccess)
            return Result<DeleteResponse>.Fail(picture.Error);

        try
        {
            _pictureRepository.Delete(picture.Value!);

            await _unitOfWork.CommitAsync();

            _logger.LogInformation(
                $"{typeof(TPicture).Name} deleted successfully. " +
                "Id: {Id}",
                picture.Value!.Id);

            await InvalidateCache(picture.Value!);

            return Result<DeleteResponse>.Success(
               new DeleteResponse(
                   picture.Value!.Id,
                   DateTime.UtcNow
               )
           );
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               $"Failed to delete {typeof(TPicture).Name}. " + 
               "Id: {Id}",
               id);

            throw;
        }    
    }
}
