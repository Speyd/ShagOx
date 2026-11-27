using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.Orders.Order;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Advertisements.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Payment;

public partial class OrderPaymentService
{
    private async Task<Result<bool>> ReserveStockAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        foreach (var item in order.OrderItems)
        {
            var advert = item.Advertisement;

            var isAvailable =
                advert.EntityStatus == EntityStatus.Active &&
                advert.Status.Code == AdvertisementStatus.Active &&
                (item.AdvertisementVariant is null ||
                 item.AdvertisementVariant.EntityStatus == EntityStatus.Active);

            if (!isAvailable)
            {
                return Result<bool>.Fail(string.Format(
                    EntityErrorResources.ProoductNotAvailable,
                    advert.Title));
            }

            var reserved = item.AdvertisementVariantId.HasValue
                ? await _variantCommand.DecreaseStockAsync(
                    item.AdvertisementVariantId.Value,
                    item.Quantity,
                    cancellationToken)
                : await _advertCommand.DecreaseStockAsync(
                    item.AdvertisementId,
                    item.Quantity,
                    cancellationToken);

            if (!reserved)
            {
                return Result<bool>.Fail(string.Format(
                    OrderResources.InsufficientStock,
                    advert.Title));
            }
        }

        return Result<bool>.Success(true);
    }
}
