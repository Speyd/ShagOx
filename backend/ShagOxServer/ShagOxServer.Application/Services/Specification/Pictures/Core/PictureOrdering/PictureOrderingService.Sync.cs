using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.PictureOrdering;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.PictureOrdering;
public partial class PictureOrderingService<TOwner, TPicture>
    : IPictureOrderingService<TOwner, TPicture>
{
    public async Task<Result<PictureSyncResponce<TPicture>>> SyncPicturesAsync(
        long ownerId,
        List<OwnerPictureOrderUpdateRequest>? request,
        CancellationToken cancellationToken = default)
    {
        var owner = await _ownerValidator
            .GetByIdAsync(ownerId, cancellationToken);

        if (!owner.IsSuccess)
        {
            return Result<PictureSyncResponce<TPicture>>
                .Fail(owner.Error);
        }

        return await SyncPicturesAsync(
            owner.Value!,
            request,
            cancellationToken);
    }

    public async Task<Result<PictureSyncResponce<TPicture>>> SyncPicturesAsync(
        TOwner owner,
        List<OwnerPictureOrderUpdateRequest>? request,
        CancellationToken cancellationToken = default)
    {
        var loadedPictures = new List<PictureCreateResponse>();
        var deletePublicIds = new List<string>();
        var updatePictures = new Dictionary<TPicture, PictureUpdateResponse>();
        var originalPictureOrders = new List<int>();

        try
        {
            if (request is null ||
                !request.Any())
            {
                return Result<PictureSyncResponce<TPicture>>
                    .Success(new PictureSyncResponce<TPicture>(
                    loadedPictures,
                    deletePublicIds,
                    updatePictures,
                    originalPictureOrders,
                    true)
                );
            }

            originalPictureOrders = 
                await PrepareOrdersAsync(owner);


            foreach (var picture in request)
            {
                var result = await SyncPictureAsync(
                    owner,
                    picture,
                    loadedPictures,
                    deletePublicIds,
                    updatePictures,
                    cancellationToken
                 );

                if (!result.IsSuccess)
                {
                    return Result<PictureSyncResponce<TPicture>>
                        .Fail(result.Error);
                }
            }

            return Result<PictureSyncResponce<TPicture>>
                .Success(new PictureSyncResponce<TPicture>(
                loadedPictures,
                deletePublicIds,
                updatePictures,
                originalPictureOrders,
                true)
            );
        }
        catch
        {
            await DeleteLoadedPicturesAsync(loadedPictures);

            ToOriginalOrder(owner, originalPictureOrders);

            throw;
        }
    }

    public async Task Rollback(
        TOwner owner,
        PictureSyncResponce<TPicture> responce)
    {
        await DeleteLoadedPicturesAsync(responce.LoadedPicture);

        await RollbackUploadedPicturesAsync(responce.UpdatePictures);

        ToOriginalOrder(owner, responce.OriginalPictureOrders);
    }

    private async Task<Result<bool>> SyncPictureAsync(
        TOwner owner,
        OwnerPictureOrderUpdateRequest picture,
        List<PictureCreateResponse> loadedPicture,
        List<string> deletePublicIds,
        Dictionary<TPicture, PictureUpdateResponse> updatePicture,
        CancellationToken cancellationToken = default)
    {
        var getImage = owner.Pictures.FirstOrDefault(
            x => x.Id == picture.Id);

        Result<bool>? result = null;

        if (picture.IsDeleted &&
            picture.Id is not null)
        {
            result = await DeletePictureAsync(
               picture,
               getImage,
               deletePublicIds,
               cancellationToken);
        }
        else if (picture.Id is null &&
            !picture.IsDeleted &&
            picture.File is not null)
        {
            result = await CreatePictureAsync(
                owner,
                picture,
                loadedPicture,
                cancellationToken);
        }
        else if (picture.Id is not null)
        {
            var updateResult = await UpdateImage(
                owner.Id,
                picture,
                getImage,
                cancellationToken);

            if (updateResult.IsSuccess)
            {
                if (getImage is not null)
                { 
                    updatePicture
                        .Add(getImage, updateResult.Value!);
                }
            }
            else
            {
                result = Result<bool>
                    .Fail(updateResult.Error);
            }
        }

        if (result is not null &&
            !result.IsSuccess)
        {
            return result;
        }

        return Result<bool>.Success(true);
    }
}
