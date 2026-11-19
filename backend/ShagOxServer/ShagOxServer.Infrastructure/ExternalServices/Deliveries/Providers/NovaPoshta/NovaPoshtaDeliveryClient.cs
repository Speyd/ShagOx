using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Query;
using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Request;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Query;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Request;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Providers;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Providers.NovaPoshta;
using ShagOxServer.Application.Resources.Delivery.Providers.NovaPoshta;
using ShagOxServer.Application.Services.Caches.Keys.Deliveries.Providers.NovaPoshta;
using ShagOxServer.Application.Services.Deliveries.Providers.NovaPoshta.Mapping;
using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Infrastructure.ExternalServices.Deliveries.Providers.NovaPoshta;
public class NovaPoshtaDeliveryClient
    : IDeliveryProviderClient
{
    private readonly NovaPoshtaApiClient _apiClient;
    private readonly INovaPoshtaTypeResolver _typeResolve;
    private readonly IDataCacheService _cacheService;
    private readonly CacheOptions _cacheSettings;


    public string ProviderCode => "NOVA_POSHTA";
    public string CurrencyCode => "UAH";

    private const decimal MinWeight = 0.1m;
    

    public NovaPoshtaDeliveryClient(
        NovaPoshtaApiClient apiClient,
        INovaPoshtaTypeResolver typeResolve,
        IDataCacheService cacheService,
        IOptions<CacheOptions> settings)
    {
        _apiClient = apiClient;
        _typeResolve = typeResolve;
        _cacheService = cacheService;
        _cacheSettings = settings.Value;
    }


    public async Task<Result<decimal>> CalculatePriceAsync(
        DeliveryCalculationRequest request,
        CancellationToken cancellationToken = default)
    {
        var serviceType = request.DeliveryType == DeliveryType.HomeDelivery
            ? "WarehouseDoors"
            : "WarehouseWarehouse";

        var methodProperties = new Dictionary<string, object>
        {
            ["CitySender"] = "8d5a980d-391c-11dd-90d9-001a92567626",
            ["CityRecipient"] = request.RecipientCityExternalId,
            ["Weight"] = Math.Max(request.Weight, MinWeight).ToString(
                System.Globalization.CultureInfo.InvariantCulture),
            ["ServiceType"] = serviceType,
            ["Cost"] = request.DeclaredValue,
            ["CargoType"] = "Parcel",
            ["SeatsAmount"] = request.SeatsAmount.ToString()
        };

        var result = await _apiClient.SendAsync<NovaPoshtaPriceData>(
            "InternetDocument",
            "getDocumentPrice",
            methodProperties,
            cancellationToken);

        if (!result.IsSuccess)
            return Result<decimal>.Fail(result.Error);

        if (result.Value?.Data is not { Count: > 0 })
        {
            return Result<decimal>.Fail(
                NovaPoshtaErrorResources.PriceCalculationNoData);
        }

        return Result<decimal>.Success(result.Value.Data[0].Cost);
    }

    public async Task<Result<IEnumerable<DeliveryCityDto>>> GetCitiesAsync(
        string search,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        var key = NovaPoshtaCache.CityBySearch(
            search,
            pagination.Page,
            pagination.PageSize);

        return await _cacheService.GetOrCreateAsync
            (key,
            async () =>
            {
                var methodProperties = new Dictionary<string, object>
                {
                    ["Page"] = pagination.Page.ToString(),
                    ["Limit"] = pagination.PageSize.ToString(),
                };

                if (!string.IsNullOrWhiteSpace(search))
                    methodProperties["FindByString"] = search;

                var result = await _apiClient.SendAsync<NovaPoshtaCityDto>(
                    "Address",
                    "getCities",
                    methodProperties,
                    cancellationToken);

                if (!result.IsSuccess)
                {
                    return Result<IEnumerable<DeliveryCityDto>>
                        .Fail(result.Error);
                }

                return Result<IEnumerable<DeliveryCityDto>>.Success(
                    (result.Value?.Data ?? [])
                        .Select(NovaPoshtaLocationMapper.MapCity)
                        .ToList()
                );
            },
            _cacheSettings.KeyExpiration);
    }

    public async Task<Result<IEnumerable<DeliveryStreetDto>>> GetStreetsAsync(
        DeliveryStreetSearchRequest request,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        var key = NovaPoshtaCache.StreetBySearch(
            request.CityExternalId,
            request.Search,
            pagination.Page,
            pagination.PageSize);

        return await _cacheService.GetOrCreateAsync
            (key,
            async () =>
            {
                var methodProperties = new Dictionary<string, object>
                {
                    ["CityRef"] = request.CityExternalId,
                    ["FindByString"] = request.Search,
                    ["Page"] = pagination.Page.ToString(),
                    ["Limit"] = pagination.PageSize.ToString(),
                };

                var result = await _apiClient.SendAsync<NovaPoshtaStreetDto>(
                    "Address",
                    "getStreet",
                    methodProperties,
                    cancellationToken);

                if (!result.IsSuccess)
                {
                    return Result<IEnumerable<DeliveryStreetDto>>
                        .Fail(result.Error);
                }

                return Result<IEnumerable<DeliveryStreetDto>>.Success(
                    (result.Value?.Data ?? [])
                        .Select(NovaPoshtaLocationMapper.MapStreet)
                        .ToList()
                );
            },
            _cacheSettings.KeyExpiration);
    }


    public async Task<Result<IEnumerable<DeliveryPointDto>>> GetPointsAsync(
        DeliveryPointSearchRequest request,
        PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var key = NovaPoshtaCache.PointBySearch(
            request.CityExternalId,
            request.Search,
            request.DeliveryPointType,
            pagination.Page,
            pagination.PageSize);

        return await _cacheService.GetOrCreateAsync
            (key,
            async () =>
            {
                var warehouseTypes = await _typeResolve
            .GetWarehouseTypesAsync(cancellationToken);

                if (!warehouseTypes.IsSuccess ||
                    warehouseTypes.Value is null)
                {
                    return Result<IEnumerable<DeliveryPointDto>>
                        .Fail(warehouseTypes.Error);
                }

                var typeOfWarehouseRef = _typeResolve
                    .GetWarehouseTypeRef(
                        request.DeliveryPointType,
                        warehouseTypes.Value);


                var methodProperties = new Dictionary<string, object>
                {
                    ["CityRef"] = request.CityExternalId,
                    ["Page"] = pagination.Page.ToString(),
                    ["Limit"] = pagination.PageSize.ToString(),
                    ["TypeOfWarehouseRef"] = typeOfWarehouseRef
                };

                if (!string.IsNullOrWhiteSpace(request.Search))
                    methodProperties["FindByString"] = request.Search;

                var result = await _apiClient.SendAsync<NovaPoshtaWarehouseDto>(
                    "Address",
                    "getWarehouses",
                    methodProperties,
                    cancellationToken);

                if (!result.IsSuccess)
                {
                    return Result<IEnumerable<DeliveryPointDto>>
                        .Fail(result.Error);
                }

                return Result<IEnumerable<DeliveryPointDto>>.Success(
                    (result.Value?.Data ?? [])
                        .Select(NovaPoshtaLocationMapper.MapPoint)
                        .ToList()
                );
            },
            _cacheSettings.KeyExpiration);
    }
}
