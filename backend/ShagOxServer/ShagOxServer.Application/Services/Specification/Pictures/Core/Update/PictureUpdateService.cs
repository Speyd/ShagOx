using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
public abstract class PictureUpdateService<TOwner, TPicture, TRequest>
    : IPictureUpdateService<TPicture, TRequest>
    where TOwner : BaseEntity
    where TPicture : BasePicture<TOwner>
    where TRequest : PictureUpdateRequest
{
    protected readonly BaseValidator<TOwner> _ownerValidator;
    protected readonly BaseValidator<TPicture> _pictureValidator;


    public PictureUpdateService(
        BaseValidator<TOwner> ownerValidator,
        BaseValidator<TPicture> pictureValidator)
    {
        _ownerValidator = ownerValidator;
        _pictureValidator = pictureValidator;
    }


    protected async Task<Result<bool>> ValidateOwnerAsync(
        Avatar avatar,
        PictureUpdateRequest request)
    {
        if (request.OwnerId is null ||
            avatar.OwnerId == request.OwnerId)
        {
            return Result<bool>.Success(true);
        }

        var existsValidator = await _ownerValidator
            .ExistsByIdAsync(request.OwnerId.Value);

        return existsValidator;
    }

    public abstract Task<Result<UpdateResponse>> UpdateInternalAsync(
        TPicture entity,
        TRequest request);

    public abstract Task<Result<UpdateResponse>> UpdateAsync(
        long id,
        TRequest request);
}
