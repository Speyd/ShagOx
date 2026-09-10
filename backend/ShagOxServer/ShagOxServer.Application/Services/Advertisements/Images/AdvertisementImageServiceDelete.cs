using ShagOxServer.Application.DTOs.Advertisements.Core.Update.Images;
using ShagOxServer.Application.DTOs.Specification.Pictures.Create;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Images;
public partial class AdvertisementImageService
{
    private async Task<Result<bool>> DeleteImageAsync(
        ImageAdvertUpdateRequest image,
        Image? getImage,
        List<string> deletePublicIds)
    {
        if (getImage is null)
        {
            return Result<bool>
                .NotFound($"Image({image.Id})");
        }

        var result = await _imageDeleteService
            .DeleteRecordAsync(image.Id!.Value);

        if (!result.IsSuccess)
        {
            return Result<bool>
                .Fail(result.Error!);
        }

        await _unitOfWork.SaveChangesAsync();

        deletePublicIds.Add(getImage.PublicId);

        return Result<bool>.Success(true);
    }


    private async Task DeleteLoadedImagesAsync(
        List<PictureCreateResponse> loadedImage)
    {
        foreach (var image in loadedImage)
        {
            await _imageLoaderService
                .DeleteAsync(image.PublicId);
        }
    }

    private async Task DeleteImagesByPublicIdAsync(
        List<string> publicIds)
    {
        foreach (var publicId in publicIds)
        {
            await _imageLoaderService.DeleteAsync(publicId);
        }
    }
}