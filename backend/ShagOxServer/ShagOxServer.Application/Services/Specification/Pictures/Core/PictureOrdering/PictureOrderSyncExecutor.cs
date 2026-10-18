using ShagOxServer.Application.DTOs.Specification.Pictures.Core.PictureOrdering;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Core.PictureOrdering;
public class PictureOrderSyncExecutor<TOwner, TPicture>
    where TOwner : BaseEntity, IHasPictures<TOwner, TPicture>
    where TPicture : BasePictureOrder<TOwner>
{
    private readonly IPictureOrderingService<TOwner, TPicture>
        _pictureOrder;

    private readonly IUnitOfWork _unitOfWork;


    public PictureOrderSyncExecutor(
        IPictureOrderingService<TOwner, TPicture> pictureOrder,
        IUnitOfWork unitOfWork)
    {
        _pictureOrder = pictureOrder;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<PictureSyncResponce<TPicture>>> SyncAsync(
        TOwner owner,
        List<OwnerPictureOrderUpdateRequest>? pictures)
    {
        var result = await _pictureOrder.SyncPicturesAsync(
            owner.Id,
            pictures);

        if (!result.IsSuccess)
        {
            await _pictureOrder.Rollback(
                owner,
                result.Value!);

            await _unitOfWork.RollbackAsync();

            return Result<PictureSyncResponce<TPicture>>
                .Fail(result.Error);
        }

        return result;
    }

    public async Task CommitAndDeleteAsync(
        PictureSyncResponce<TPicture> response)
    {
        await _unitOfWork.CommitAsync();

        await _pictureOrder.DeletePicturesByPublicIdAsync(
            response.DeletePublicIds);
    }
}
