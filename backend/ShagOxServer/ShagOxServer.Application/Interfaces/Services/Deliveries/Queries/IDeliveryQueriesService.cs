using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Query;
using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Request;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Request;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.Queries;
public interface IDeliveryQueriesService
{
    Task<Result<IEnumerable<DeliveryCityDto>>> GetCitiesAsync(
        string providerCode,
        string citySearch,
        PaginationParams pagination,
        CancellationToken cancellationToken);

    Task<Result<IEnumerable<DeliveryStreetDto>>> GetStreetsAsync(
        string providerCode,
        DeliveryStreetSearchRequest request,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<DeliveryPointDto>>> GetPointsAsync(
        string providerCode,
        DeliveryPointSearchRequest request,
        PaginationParams pagination,
        CancellationToken cancellationToken);

    string GetTrackingUrl(
        string providerCode,
        string number);
}
