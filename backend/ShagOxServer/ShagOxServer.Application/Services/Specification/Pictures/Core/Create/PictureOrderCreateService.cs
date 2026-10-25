using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Core.Create;
public abstract class PictureOrderCreateService<TOwner, TPicture, TRequest>
    : PictureCreateService<TOwner, TPicture, TRequest>,
    IPictureOrderCreateService<TPicture, TRequest>
    where TOwner : BaseEntity
    where TPicture : BasePictureOrder
    where TRequest : PictureOrderCreateRequest
{
    public PictureOrderCreateService(
        IRepository<TPicture> pictureRepository,
        PictureValidator pictureValidator,
        BaseValidator<TOwner> ownerValidator,
        IUnitOfWork unitOfWork,
        ILogger<PictureOrderCreateService<TOwner,
            TPicture, TRequest>> logger
    )
        : base(pictureRepository, 
            pictureValidator, 
            ownerValidator,
            unitOfWork, 
            logger)
    {
    }


    protected abstract TRequest CreatePictureOrderRequest(
        IFormFile file,
        long ownerId,
        int order);

    public virtual async Task<Result<PicturesCreateResponse>> CreateAsync(
        PicturesCreateRequest request)
    {
        var uploadedPictures = new List<string>();

        int order = 0;

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            foreach (var file in request.Files)
            {
                var result = await CreateInternalAsync(
                    CreatePictureOrderRequest(
                        file,
                        request.OwnerId,
                        order++));


                if (!result.IsSuccess)
                    throw new Exception("Fail picture create");

                uploadedPictures.Add(result.Value!.PublicId);
            }

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               $"Failed to create {typeof(TPicture).Name}" +
               "(Count:{Count}). OwnerId: {OwnerId}",
               request.Files.Count,
               request.OwnerId);

            foreach (var picture in uploadedPictures)
            { 
                await _pictureValidator
                    .PictureDeleteValidator(picture);
            }

            throw;
        }

        return Result<PicturesCreateResponse>.Success(
            new PicturesCreateResponse(
                uploadedPictures.Count,
                DateTime.UtcNow
        ));
    }
}
