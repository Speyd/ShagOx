using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
public interface IDeliveryPriceCalculator
{
    Task<decimal> CalculateAsync(
        Order order,
        CancellationToken cancellationToken = default);
}
