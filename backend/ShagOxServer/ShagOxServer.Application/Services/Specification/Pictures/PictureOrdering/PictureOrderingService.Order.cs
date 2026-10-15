using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.PictureOrdering;
public partial class PictureOrderingService<TOwner, TPicture>
    : IPictureOrderingService<TOwner, TPicture>
{
    private const int TemporaryOrderOffset = -1_000_000;

    private async Task<List<int>> PrepareOrdersAsync(
        TOwner owner)
    {
        var originalPictureOrders = new List<int>();

        var temporaryOrder = TemporaryOrderOffset;
        foreach (var picture in owner.Pictures)
        {
            originalPictureOrders.Add(picture.Order);
            picture.Order = temporaryOrder++;
        }

        await _unitOfWork.SaveChangesAsync();

        return originalPictureOrders;
    }

    private void ToOriginalOrder(
        TOwner owner,
        List<int> originalPictureOrders)
    {
        if (originalPictureOrders.Count != owner.Pictures.Count)
            return;

        for (int i = 0; i < owner.Pictures.Count; i++)
        {
            owner.Pictures.ElementAt(i).Order 
                = originalPictureOrders[i];
        }
    }


    private Result<bool> UpdateImageOrder(
        OwnerPictureOrderUpdateRequest picture,
        TPicture? getPicture)
    {
        if (getPicture is not null)
        {
            getPicture.Order = picture.Order;
        }

        return Result<bool>.Success(true);
    }
}
