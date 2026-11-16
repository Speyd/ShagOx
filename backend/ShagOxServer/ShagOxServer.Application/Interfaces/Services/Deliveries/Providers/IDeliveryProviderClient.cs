using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Query;
using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Request;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Request;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.Providers;
public interface IDeliveryProviderClient
{
    string ProviderCode { get; }
    string CurrencyCode { get; }
    string CityLanguageCode { get; }

    Task<decimal> CalculatePriceAsync(
        DeliveryCalculationRequest request,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<DeliveryCityDto>> GetCitiesAsync(
        string search,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<DeliveryStreetDto>> GetStreetsAsync(
        DeliveryStreetSearchRequest request,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<DeliveryPointDto>> GetPointsAsync(
        DeliveryPointSearchRequest request,
        PaginationParams pagination,
        CancellationToken cancellationToken);
}
