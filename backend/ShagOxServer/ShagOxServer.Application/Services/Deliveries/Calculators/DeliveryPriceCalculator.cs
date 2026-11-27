using ShagOxServer.Application.DTOs.Deliveries;
using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Request;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
using ShagOxServer.Application.Resources.Delivery.Core;
using ShagOxServer.Application.Services.Deliveries.Providers;
using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Deliveries.Calculators;
public class DeliveryPriceCalculator
    : IDeliveryPriceCalculator
{
    private readonly ICurrencyConverter _converter;
    private readonly DeliveryProviderResolver _resolver;


    public DeliveryPriceCalculator(
        ICurrencyConverter converter,
        DeliveryProviderResolver resolver)
    {
        _converter = converter;
        _resolver = resolver;
    }


    public async Task<Result<decimal>> CalculateAsync(
        Order order,
        CancellationToken cancellationToken = default)
    {

        if (order.DeliveryType == DeliveryType.Pickup)
            return Result<decimal>.Success(0m);

        var providerCode = order.DeliveryProvider?.Code;

        if (string.IsNullOrWhiteSpace(providerCode))
            return Result<decimal>.Success(0m);

        var provider = _resolver.Resolve(providerCode);

        var declaredValue = ConvertDeclaredValue(
            order,
            provider.CurrencyCode);

        if (!declaredValue.IsSuccess)
            return Result<decimal>.Fail(declaredValue.Error);


        var request = CreateCalculationRequest(
            order,
            declaredValue.Value!.ConvertedAmount!.Value);

        if (!request.IsSuccess || request.Value is null)
            return Result<decimal>.Fail(request.Error);

        var deliveryPrice = await provider.CalculatePriceAsync(
            request.Value,
            cancellationToken);

        if (!deliveryPrice.IsSuccess)
            return Result<decimal>.Fail(deliveryPrice.Error);

        var price = deliveryPrice.Value / declaredValue.Value.Coefficient;

        return Result<decimal>.Success(price);
    }

    private Result<AmountConversionResult> ConvertDeclaredValue(
        Order order,
        string targetCurrency)
    {
        if (order.Currency.Code == targetCurrency)
        {
            return Result<AmountConversionResult>
                .Success(new AmountConversionResult(
                    order.Subtotal, 
                    1));
        }

        var result = _converter.Convert(
            order.Subtotal,
            order.Currency.Code,
            targetCurrency);

        if (!result.ConvertedAmount.HasValue) 
        {
            return Result<AmountConversionResult>.Fail(
                DeliveryErrorResources.FailedToConvertOrderSubtotal);
        }

        return Result<AmountConversionResult>
            .Success(result);
    }

    private static Result<DeliveryCalculationRequest> CreateCalculationRequest(
        Order order,
        decimal declaredValue)
    {
        if(string.IsNullOrEmpty(order.RecipientCityExternalId))
        {
            return Result<DeliveryCalculationRequest>.Fail(
                DeliveryErrorResources.FailedToCreateDeliveryRequest);
        }

        return Result<DeliveryCalculationRequest>.Success(
            new DeliveryCalculationRequest(
                order.DeliveryType,
                order.DeliveryPointType,
                order.RecipientCityExternalId,
                order.DeliveryPointExternalId,
                order.TotalWeight,
                declaredValue));
    }
}
