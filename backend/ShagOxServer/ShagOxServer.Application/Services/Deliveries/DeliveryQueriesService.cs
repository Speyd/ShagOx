using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Query;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Request;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Queries;
using ShagOxServer.Application.Services.Deliveries.Providers;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Deliveries;
public class DeliveryQueriesService
    : IDeliveryQueriesService
{
    private readonly DeliveryProviderResolver _resolver;


    public DeliveryQueriesService(
        DeliveryProviderResolver resolver)
    {
        _resolver = resolver;
    }


    public async Task<Result<IEnumerable<DeliveryCityDto>>> GetCitiesAsync(
        string providerCode, 
        string citySearch, 
        CancellationToken cancellationToken)
    {
        var delivaery = _resolver.Resolve(providerCode);

        var result = await delivaery.GetCitiesAsync(
            citySearch,
            cancellationToken);

        return Result<IEnumerable<DeliveryCityDto>>
            .Success(result);
    }

    public async Task<Result<IEnumerable<DeliveryPointDto>>> GetPointsAsync(
        string providerCode,
        DeliveryPointSearchRequest request,
        PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var delivaery = _resolver.Resolve(providerCode);

        var result = await delivaery.GetPointsAsync(
            request,
            pagination,
            cancellationToken);

        return Result<IEnumerable<DeliveryPointDto>>
            .Success(result);
    }
}

