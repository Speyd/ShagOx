using ShagOxServer.Application.DTOs.Specification.Pictures.Core.PictureOrdering;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
public interface IPictureOrderingService<TOwner, TPicture>
    where TOwner: BaseEntity, IHasPictures<TOwner, TPicture>
    where TPicture: BasePictureOrder<TOwner>   
{
    Task<Result<PictureSyncResponce<TPicture>>> SyncPicturesAsync(
        long ownerId,
        List<OwnerPictureOrderUpdateRequest>? request);

    Task<Result<PictureSyncResponce<TPicture>>> SyncPicturesAsync(
        TOwner advertisement,
        List<OwnerPictureOrderUpdateRequest>? request);

    Task DeletePicturesByPublicIdAsync(
        List<string> publicIds);

    Task Rollback(
        TOwner owner,
        PictureSyncResponce<TPicture> responce);
}
