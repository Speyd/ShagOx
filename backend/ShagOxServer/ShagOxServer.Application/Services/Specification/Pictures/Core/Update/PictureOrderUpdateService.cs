using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
public abstract class PictureOrderUpdateService<TOwner, TPicture, TRequest>
    : IPictureUpdateService<TPicture, TRequest>
    where TOwner : BaseEntity, IHasPictures<TOwner, TPicture>
    where TPicture : BasePictureOrder<TOwner>
    where TRequest : PictureUpdateRequest
{
    protected readonly BaseValidator<TOwner> _ownerValidator;
    protected readonly BaseValidator<TPicture> _pictureValidator;


    public PictureOrderUpdateService(
        BaseValidator<TOwner> ownerValidator,
        BaseValidator<TPicture> pictureValidator)
    {
        _ownerValidator = ownerValidator;
        _pictureValidator = pictureValidator;
    }


    protected virtual async Task<Result<int?>> GetNewOrder(
        long pictureId,
        PictureOrderUpdateRequest request)
    {
        int? newOrder = request.Order;

        if (request.OwnerId is not null &&
            request.Order is not null)
        {
            var owner = await _ownerValidator
                .GetByIdAsync(request.OwnerId.Value);

            var orderExists = owner.Value!.Pictures.Any(x =>
                x.Id != pictureId &&
                x.Order == request.Order);

            if (orderExists)
            {
                newOrder = BasePictureOrder<TOwner>
                    .GetNextOrder(owner.Value!);
            }
        }

        return Result<int?>.Success(newOrder);
    }

    public abstract Task<Result<UpdateResponse>> UpdateInternalAsync(
        TPicture entity,
        TRequest request);

    public abstract Task<Result<UpdateResponse>> UpdateAsync(
        long id,
        TRequest request);
}
