using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Query;
using ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Request;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Query;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Request;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Providers;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Providers.NovaPoshta;
using ShagOxServer.Application.Services.Deliveries.Providers.NovaPoshta.Mapping;
using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.ExternalServices.Deliveries.Providers.NovaPoshta;
public class NovaPoshtaDeliveryClient
    : IDeliveryProviderClient
{
    private readonly NovaPoshtaApiClient _apiClient;
    private readonly INovaPoshtaTypeResolver _typeResolve;

    public string ProviderCode => "NOVA_POSHTA";
    public string CurrencyCode => "UAH";
    public string CityLanguageCode => "uk";
    

    public NovaPoshtaDeliveryClient(
        NovaPoshtaApiClient apiClient,
        INovaPoshtaTypeResolver typeResolve)
    {
        _apiClient = apiClient;
        _typeResolve = typeResolve;
    }


    public async Task<decimal> CalculatePriceAsync(
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
            ["Weight"] = request.Weight.ToString(
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

        if (result?.Data is not { Count: > 0 })
            throw new InvalidOperationException(
                "Nova Poshta price calculation returned no data.");

        return result.Data[0].Cost;
    }

    public async Task<IEnumerable<DeliveryCityDto>> GetCitiesAsync(
        string search,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
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

        return (result?.Data ?? [])
            .Select(NovaPoshtaLocationMapper.MapCity)
            .ToList();
    }

    public async Task<IEnumerable<DeliveryStreetDto>> GetStreetsAsync(
        DeliveryStreetSearchRequest request,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
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

        return (result?.Data ?? [])
            .Select(x => new DeliveryStreetDto(
                x.Ref,
                x.Description))
            .ToList();
    }


    public async Task<IEnumerable<DeliveryPointDto>> GetPointsAsync(
        DeliveryPointSearchRequest request,
        PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var warehouseTypes = await _typeResolve
            .GetWarehouseTypesAsync(cancellationToken);

        var typeOfWarehouseRef = _typeResolve
            .GetWarehouseTypeRef(
                request.DeliveryPointType,
                warehouseTypes);


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

        return (result?.Data ?? [])
            .Select(NovaPoshtaLocationMapper.MapPoint)
            .ToList();
    }
}
