using ShagOxServer.Application.DTOs.Advertisements.Update.Images;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Images;
public partial class AdvertisementImageService
{
    private const int TemporaryOrderOffset = -1_000_000;

    private async Task<List<int>> PrepareOrdersAsync(
        Advertisement advertisement)
    {
        var originalImageOrders = new List<int>();

        var temporaryOrder = TemporaryOrderOffset;
        foreach (var image in advertisement.Images)
        {
            originalImageOrders.Add(image.Order);
            image.Order = temporaryOrder++;
        }

        await _unitOfWork.SaveChangesAsync();

        return originalImageOrders;
    }

    private void ToOriginalOrder(
        Advertisement advertisement,
        List<int> originalImageOrders)
    {
        if (originalImageOrders.Count != advertisement.Images.Count)
            return;

        for (int i = 0; i < advertisement.Images.Count; i++)
        {
            advertisement.Images[i].Order = originalImageOrders[i];
        }
    }


    private Result<bool> UpdateImageOrder(
        ImageAdvertUpdateRequest image,
        Image? getImage)
    {
        if (getImage is not null)
        {
            getImage.Order = image.Order;
        }

        return Result<bool>.Success(true);
    }
}