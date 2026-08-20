using ShagOxServer.Application.DTOs.Advertisements.Update.Images;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Create.File;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Images;
public partial class AdvertisementImageService
{
    private async Task<Result<bool>> CreateImageAsync(
        Advertisement advertisement,
        ImageAdvertUpdateRequest image,
        List<ImageCreateResponse> loadedImage)
    {
        var result = await _imageCreateService
            .CreateFromFileAsync(advertisement,
                new ImageFileCreateRequest(
                    image.File!,
                    advertisement.Id,
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