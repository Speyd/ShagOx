using ShagOxServer.Application.DTOs.Advertisements.Update.Images;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Create.File;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Images;
public partial class AdvertisementImageService
{
    private async Task<Result<bool>> CreateImageAsync(
        int advertisementId,
        ImageAdvertUpdateRequest image,
        List<ImageCreateResponse> loadedImage)
    {
        var result = await _imageCreateService
            .CreateFromFileAsync(
                new ImageFileCreateRequest(
                    image.File!,
                    advertisementId,
                    image.Order));

        if (!result.IsSuccess)
        {
            return Result<bool>
                .Fail(result.Error!);
        }

        if (result.Value is not null)
        {
            loadedImage.Add(result.Value);
        }

        return Result<bool>.Success(true);
    }
}