using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
public interface IDeliveryPriceCalculator
{
    Task<Result<decimal>> CalculateAsync(
        Order order,
        CancellationToken cancellationToken = default);
}
