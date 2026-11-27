using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Query;
using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Request;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Request;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Providers;
public interface IDeliveryProviderClient
{
    string ProviderCode { get; }
    string CurrencyCode { get; }

    Task<Result<decimal>> CalculatePriceAsync(
        DeliveryCalculationRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<DeliveryCityDto>>> GetCitiesAsync(
        string search,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<DeliveryStreetDto>>> GetStreetsAsync(
        DeliveryStreetSearchRequest request,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<DeliveryPointDto>>> GetPointsAsync(
        DeliveryPointSearchRequest request,
        PaginationParams pagination,
        CancellationToken cancellationToken);

    string GetTrackingUrl(
        string? number);
}
