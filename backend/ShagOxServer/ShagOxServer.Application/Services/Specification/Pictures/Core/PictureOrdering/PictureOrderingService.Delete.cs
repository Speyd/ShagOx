using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.PictureOrdering;
public partial class PictureOrderingService<TOwner, TPicture>
    : IPictureOrderingService<TOwner, TPicture>
{
    private async Task<Result<bool>> DeletePictureAsync(
        OwnerPictureOrderUpdateRequest picture,
        TPicture? getPicture,
        List<string> deletePublicIds)
    {
        if (getPicture is null)
        {
            return Result<bool>.NotFound(
                string.Format(EntityNamesResources.DefiniteImage,
                picture.Id));
        }

        var result = await _pictureDeleteService
            .DeleteRecordAsync(picture.Id!.Value);

        if (!result.IsSuccess)
        {
            return Result<bool>
                .Fail(result.Error!);
        }

        await _unitOfWork.SaveChangesAsync();

        deletePublicIds.Add(getPicture.PublicId);

        return Result<bool>.Success(true);
    }


    private async Task DeleteLoadedPicturesAsync(
        List<PictureCreateResponse> loadedPictures)
    {
        foreach (var picture in loadedPictures)
        {
            await _pictureLoaderService
                .DeleteAsync(picture.PublicId);
        }
    }

    public async Task DeletePicturesByPublicIdAsync(
        List<string> publicIds)
    {
        foreach (var publicId in publicIds)
        {
            await _pictureLoaderService.DeleteAsync(publicId);
        }
    }
}
