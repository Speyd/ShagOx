using ShagOxServer.Application.DTOs.Deliveries.Calculators;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.Domain.Entities.Orders;

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


    public async Task<decimal> CalculateAsync(
        Order order,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(order);

        if (order.DeliveryType == DeliveryType.Pickup)
            return 0m;

        var providerCode = order.DeliveryProvider?.Code;

        if (string.IsNullOrWhiteSpace(providerCode))
            return 0m;

        var provider = _resolver.Resolve(providerCode);

        var declaredValue = ConvertDeclaredValueAsync(
            order,
            provider.CurrencyCode);

        var request = CreateCalculationRequest(
            order,
            declaredValue);

        return await provider.CalculatePriceAsync(
            request,
            cancellationToken);
    }

    private decimal ConvertDeclaredValueAsync(
        Order order,
        string targetCurrency)
    {
        if (order.Currency.Code == targetCurrency)
            return order.Subtotal;

        var result = _converter.Convert(
            order.Subtotal,
            order.Currency.Code,
            targetCurrency);

        return result.ConvertedAmount
            ?? throw new InvalidOperationException(
                "Failed to convert order subtotal.");
    }

    private static DeliveryCalculationRequest CreateCalculationRequest(
        Order order,
        decimal declaredValue)
    {
        return new DeliveryCalculationRequest(
            order.DeliveryType,
            order.DeliveryPointType,
            order.DeliveryPointExternalId,
            order.DeliveryAddress,
            order.DeliveryPointExternalId!,
            order.TotalWeight,
            declaredValue
        );
    }
}
