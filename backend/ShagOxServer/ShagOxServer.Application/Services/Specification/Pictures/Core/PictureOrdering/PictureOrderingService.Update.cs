using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.PictureOrdering;
public partial class PictureOrderingService<TOwner, TPicture>
    : IPictureOrderingService<TOwner, TPicture>
{
    private async Task<Result<PictureUpdateResponse?>> UpdateImage(
        long ownerId,
        OwnerPictureOrderUpdateRequest picture,
        TPicture? getPicture)
    {
        if (getPicture is null)
        {
            return Result<PictureUpdateResponse?>
                .Success(default);
        }

        var request = new PictureOrderUpdateRequest(
            ownerId,
            picture.File,
            picture.Order
        );

        var result = await _pictureUpdateService
            .UpdateInternalAsync(getPicture, request);

        return result!;
    }

    private async Task RollbackUploadedPicturesAsync(
       Dictionary<TPicture, PictureUpdateResponse> uplodaedPictures)
    {
        foreach (var picture in uplodaedPictures)
        {
            await _pictureUpdateService.RollbackAsync(
                picture.Key,
                picture.Value);
        }
    }
}
