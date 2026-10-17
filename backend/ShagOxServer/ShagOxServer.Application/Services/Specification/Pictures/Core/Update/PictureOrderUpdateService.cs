using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Update;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
public abstract class PictureOrderUpdateService<TOwner, TPicture, TRequest>
    : PictureUpdateService<TOwner, TPicture, TRequest>
    where TOwner : BaseEntity, IHasPictures<TOwner, TPicture>
    where TPicture : BasePictureOrder<TOwner>
    where TRequest : PictureOrderUpdateRequest
{
    protected readonly BaseValidator<TOwner> _ownerValidator;
    protected readonly BaseValidator<TPicture> _pictureValidator;


    public PictureOrderUpdateService(
        IRepository<TPicture> pictureRepository,
        BaseValidator<TPicture> pictureValidator,
        BaseValidator<TOwner> ownerValidator,
        IUnitOfWork unitOfWork,
        ILogger<PictureOrderUpdateService<TOwner, 
            TPicture, TRequest>> logger
    )
        : base(pictureRepository, 
            pictureValidator, 
            ownerValidator, 
            unitOfWork, 
            logger)
    {
        _ownerValidator = ownerValidator;
        _pictureValidator = pictureValidator;
    }


    protected virtual async Task<Result<int?>> GetNewOrder(
        long pictureId,
        TRequest request)
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

    public override async Task<Result<UpdateResponse>> UpdateInternalAsync(
        TPicture entity,
        TRequest request)
    {
        var newOrder = await GetNewOrder(entity.Id, request);

        if (!newOrder.IsSuccess)
        {
            return Result<UpdateResponse>.Fail(newOrder.Error);
        }

        if (newOrder.Value is null)
        {
            throw new Exception("Picture update error");
        }

        request = request with 
        { 
            Order = newOrder.Value
        };

        return await base.UpdateInternalAsync(entity, request);
    }
}
