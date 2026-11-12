using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Deliveries.Providers;
using ShagOxServer.Application.DTOs.Deliveries.Calculators;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
using ShagOxServer.Domain.Entities.Deliveries.Enum;
using System.Net.Http.Json;

namespace ShagOxServer.Application.Services.Deliveries.Providers.NovaPoshta;
public class NovaPoshtaDeliveryClient
    : IDeliveryProviderClient
{
    private readonly HttpClient _httpClient;
    private readonly NovaPoshtaOptions _options;


    public string ProviderCode => "NOVA_POSHTA";
    public string CurrencyCode => "UAH";


    public NovaPoshtaDeliveryClient(
        HttpClient httpClient,
        IOptions<NovaPoshtaOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<decimal> CalculatePriceAsync(
        DeliveryCalculationRequest request,
        CancellationToken cancellationToken = default)
    {
        var body = new
        {
            _options.ApiKey,
            modelName = "InternetDocument",
            calledMethod = "getDocumentPrice",
            methodProperties = new
            {
                CitySender = request.SenderCityExternalId,
                CityRecipient = request.DeliveryPointExternalId,
                Weight = request.Weight.ToString(
                    System.Globalization.CultureInfo.InvariantCulture),
                ServiceType = request.DeliveryType == DeliveryType.HomeDelivery
                    ? "WarehouseDoors"
                    : "WarehouseWarehouse",
                Cost = request.DeclaredValue,
                CargoType = "Parcel",
                SeatsAmount = "1"
            }
        };

        using var response = await _httpClient.PostAsJsonAsync(
            "",
            body,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<NovaPoshtaResponse>(
            cancellationToken: cancellationToken);

        if (result?.Success != true || result.Data.Count == 0)
            throw new InvalidOperationException(
                "Nova Poshta price calculation failed.");

        return result.Data[0].Cost;
    }
}
