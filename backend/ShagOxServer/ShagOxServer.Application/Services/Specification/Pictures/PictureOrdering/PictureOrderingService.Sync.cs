using ShagOxServer.Application.DTOs.Specification.Pictures.Core.PictureOrdering;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
using ShagOxServer.Application.DTOs.Specification.Pictures.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.PictureOrdering;
public partial class PictureOrderingService<TOwner, TPicture>
    : IPictureOrderingService<TOwner, TPicture>
{
    public async Task<Result<SyncPictureResponce>> SyncPicturesAsync(
        long ownerId,
        List<OwnerPictureOrderUpdateRequest>? request)
    {
        var owner = await _ownerValidator
            .GetByIdAsync(ownerId);

        if (!owner.IsSuccess)
            return Result<SyncPictureResponce>.Fail(owner.Error);

        return await SyncPicturesAsync(
            owner.Value!,
            request);
    }

    public async Task<Result<SyncPictureResponce>> SyncPicturesAsync(
        TOwner owner,
        List<OwnerPictureOrderUpdateRequest>? request)
    {
        var loadedPicture = new List<PictureCreateResponse>();
        var deletePublicIds = new List<string>();
        var originalPictureOrders = new List<int>();

        try
        {
            if (request is null ||
                !request.Any())
            {
                return Result<SyncPictureResponce>.Success(new SyncPictureResponce(
                    loadedPicture,
                    deletePublicIds,
                    originalPictureOrders,
                    true)
                );
            }

            originalPictureOrders = 
                await PrepareOrdersAsync(owner);


            foreach (var picture in request)
            {
                await SyncPictureAsync(
                    owner,
                    picture,
                    loadedPicture,
                    deletePublicIds
                 );
            }

            return Result<SyncPictureResponce>.Success(new SyncPictureResponce(
                loadedPicture,
                deletePublicIds,
                originalPictureOrders,
                true)
            );
        }
        catch
        {
            await DeleteLoadedPicturesAsync(loadedPicture);

            ToOriginalOrder(owner, originalPictureOrders);

            throw;
        }
    }

    public async Task Rollback(
        TOwner owner,
        SyncPictureResponce responce)
    {
        await DeleteLoadedPicturesAsync(responce.LoadedPicture);

        ToOriginalOrder(owner, responce.OriginalPictureOrders);
    }

    private async Task<Result<bool>> SyncPictureAsync(
        TOwner owner,
        OwnerPictureOrderUpdateRequest picture,
        List<PictureCreateResponse> loadedPicture,
        List<string> deletePublicIds)
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
               deletePublicIds);
        }

        if (!picture.IsDeleted &&
            picture.File is not null)
        {
            result = await CreatePictureAsync(
                owner,
                picture,
                loadedPicture);
        }

        if (picture.Id is not null)
        {
            result = UpdateImageOrder(
                picture,
                getImage);
        }

        if (result is not null &&
            !result.IsSuccess)
        {
            throw new Exception("Handle image sync errors and rollback");
        }

        return Result<bool>.Success(true);
    }
}
