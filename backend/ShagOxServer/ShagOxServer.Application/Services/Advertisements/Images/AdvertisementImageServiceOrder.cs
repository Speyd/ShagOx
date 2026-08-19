using ShagOxServer.Application.DTOs.Advertisements.Update.Images;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Images;
public partial class AdvertisementImageService
{
    private const int TemporaryOrderOffset = -1_000_000;

    private async Task<Result<bool>> PrepareOrdersAsync(
    Advertisement advertisement,
    List<ImageAdvertUpdateRequest> images)
    {
        var existingImages = advertisement.Images.ToList();

        if (!existingImages.Any())
            return Result<bool>.Success(true);

        foreach (var image in existingImages)
        {
            image.Order = TemporaryOrderOffset - image.Id;
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }


    private Result<bool> UpdateImageOrder(
        ImageAdvertUpdateRequest image,
        Image? getImage)
    {
        if (getImage is null)
        {
            return Result<bool>
                .NotFound($"Image({image.Id})");
        }

        getImage.Order = image.Order;

        return Result<bool>.Success(true);
    }
}