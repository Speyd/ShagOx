using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.PictureOrdering;
public partial class PictureOrderingService<TOwner, TPicture>
    : IPictureOrderingService<TOwner, TPicture>
{
    private async Task<Result<bool>> CreatePictureAsync(
        TOwner owner,
        OwnerPictureOrderUpdateRequest pictureRequest,
        List<PictureCreateResponse> loadedPictures)
    {
        var result = await _pictureCreateService
            .CreateInternalAsync(
                new PictureOrderCreateRequest(
                    pictureRequest.File!,
                    owner.Id,
                    pictureRequest.Order
            ));

        if (!result.IsSuccess)
        {
            return Result<bool>
                .Fail(result.Error!);
        }

        if (result.Value is not null)
        {
            loadedPictures.Add(result.Value);
        }

        return Result<bool>.Success(true);
    }
}
