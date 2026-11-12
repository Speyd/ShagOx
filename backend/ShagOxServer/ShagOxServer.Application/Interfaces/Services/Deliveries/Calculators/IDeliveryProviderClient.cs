using ShagOxServer.Application.DTOs.Deliveries.Calculators;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
public interface IDeliveryProviderClient
{
    string ProviderCode { get; }
    string CurrencyCode { get; }

    Task<decimal> CalculatePriceAsync(
        DeliveryCalculationRequest request,
        CancellationToken cancellationToken = default);
}
