using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Core.Create;

public partial class OrderCreateService 
    : IOrderCreateService
{
    private async Task<Result<(Currency, DeliveryProvider?)>> ValidateOrderAsync(
        long ownerId,
        OrderCreateRequest request,
        CancellationToken cancellationToken)
    {
        var userResult = await _userValidator
            .ExistsByIdAsync(ownerId, cancellationToken);

        if (!userResult.IsSuccess)
        {
            return Result<(Currency, DeliveryProvider?)>
                .Fail(userResult.Error);
        }

        DeliveryProvider? deliveryProvider = null;
        if (request.DeliveryProviderId.HasValue)
        {
            var deliveryResult = await _deliveryValidator
                .GetByIdAsync(
                    request.DeliveryProviderId.Value,
                    cancellationToken);

            if (!deliveryResult.IsSuccess)
            {
                return Result<(Currency, DeliveryProvider?)>
                    .Fail(deliveryResult.Error);
            }

            deliveryProvider = deliveryResult.Value;
        }

        var currencyResult = await _currencyProvider
            .GetCurrencyAsync(cancellationToken);

        if (currencyResult is null)
        {
            currencyResult = await _currencyProvider
                .GetBaseCurrencyAsync(cancellationToken);

            if (currencyResult is null)
            {
                return Result<(Currency, DeliveryProvider?)>
                    .NotFound(EntityNamesResources.Currency);
            }
        }

        return Result<(Currency, DeliveryProvider?)>.Success(
            (currencyResult, deliveryProvider));
    }
}
