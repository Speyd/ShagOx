using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Request;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
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
        ArgumentNullException.ThrowIfNull(order);

        if (order.DeliveryType == DeliveryType.Pickup)
            return Result<decimal>.Success(0m);

        var providerCode = order.DeliveryProvider?.Code;

        if (string.IsNullOrWhiteSpace(providerCode))
            return Result<decimal>.Success(0m);

        var provider = _resolver.Resolve(providerCode);

        var declaredValue = ConvertDeclaredValue(
            order,
            provider.CurrencyCode);

        var request = CreateCalculationRequest(
            order,
            declaredValue.ConvertedAmount!.Value);

        var deliveryPrice = await provider.CalculatePriceAsync(
            request,
            cancellationToken);

        if (!deliveryPrice.IsSuccess)
            return Result<decimal>.Fail(deliveryPrice.Error);

        var price = deliveryPrice.Value / declaredValue.Coefficient;

        return Result<decimal>.Success(price);
    }

    private (decimal? ConvertedAmount, decimal Coefficient) ConvertDeclaredValue(
        Order order,
        string targetCurrency)
    {
        if (order.Currency.Code == targetCurrency)
            return (order.Subtotal, 1);

        var result = _converter.Convert(
            order.Subtotal,
            order.Currency.Code,
            targetCurrency);

        //TODO: доделать ресурс
        if (!result.ConvertedAmount.HasValue) 
        {
            throw new InvalidOperationException(
                "Failed to convert order subtotal.");
        }

        return result;
    }

    private static DeliveryCalculationRequest CreateCalculationRequest(
        Order order,
        decimal declaredValue)
    {
        if(string.IsNullOrEmpty(order.RecipientCityExternalId))
        {
            throw new InvalidOperationException(
                "Failed to create delivery request.");
        }

        return new DeliveryCalculationRequest(
            order.DeliveryType,
            order.DeliveryPointType,
            order.RecipientCityExternalId,
            order.DeliveryPointExternalId,
            order.TotalWeight,
            declaredValue
        );
    }
}
