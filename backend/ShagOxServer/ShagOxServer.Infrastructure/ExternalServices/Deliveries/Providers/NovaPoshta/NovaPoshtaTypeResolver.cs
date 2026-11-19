using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.Common.Options.Deliveries.Providers;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Query;
using ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Responses;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Providers.NovaPoshta;
using ShagOxServer.Application.Resources.Delivery.Providers.NovaPoshta;
using ShagOxServer.Application.Services.Caches.Keys.Deliveries.Providers.NovaPoshta;
using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.ExternalServices.Deliveries.Providers.NovaPoshta;
public class NovaPoshtaTypeResolver
    : INovaPoshtaTypeResolver
{
    private readonly HttpClient _httpClient;
    private readonly IDataCacheService _cacheService;
    private readonly NovaPoshtaOptions _options;
    private readonly CacheOptions _cacheOptions;


    public NovaPoshtaTypeResolver(
        HttpClient httpClient,
        IDataCacheService cacheService,
        IOptions<NovaPoshtaOptions> options,
        IOptions<CacheOptions> cacheOptions)
    {
        _httpClient = httpClient;
        _cacheService = cacheService;
        _options = options.Value;
        _cacheOptions = cacheOptions.Value;
    }


    public async Task<Result<IReadOnlyList<NovaPoshtaWarehouseTypeDto>>> GetWarehouseTypesAsync(
        CancellationToken cancellationToken)
    {
        var key = NovaPoshtaCache.WarehouseTypes();

        return await _cacheService.GetOrCreateAsync
            (key,
            async () =>
            {
                var body = new
                {
                    apiKey = _options.ApiKey,
                    modelName = "Address",
                    calledMethod = "getWarehouseTypes",
                    methodProperties = new { }
                };

                var json = JsonSerializer.Serialize(body);

                using var response = await _httpClient.PostAsync(
                    "",
                    new StringContent(json, Encoding.UTF8, "application/json"),
                    cancellationToken);

                var rawResponse = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return Result<IReadOnlyList<NovaPoshtaWarehouseTypeDto>>.Fail(
                        string.Format(
                            NovaPoshtaErrorResources.HttpRequestFailed,
                            (int)response.StatusCode, rawResponse
                        ));
                }

                var result = JsonSerializer.Deserialize<
                    NovaPoshtaResponse<NovaPoshtaWarehouseTypeDto>>(
                    rawResponse,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (result?.Success != true)
                {
                    return Result<IReadOnlyList<NovaPoshtaWarehouseTypeDto>>.Fail(
                        string.Format(
                            NovaPoshtaErrorResources.HttpRequestFailed,
                            string.Join(", ", result?.Errors ?? [])
                        ));
                }

                return Result<IReadOnlyList<NovaPoshtaWarehouseTypeDto>>
                    .Success(result.Data ?? []);
            },
            _cacheOptions.KeyExpiration);
    }

    public string GetWarehouseTypeRef(
        DeliveryPointType type,
        IReadOnlyList<NovaPoshtaWarehouseTypeDto> warehouseTypes)
    {
        var matches = type switch
        {
            DeliveryPointType.Branch => warehouseTypes
                .Where(x =>
                    string.Equals(
                        x.DescriptionRu?.Trim(),
                        "Почтовое отделение",
                        StringComparison.OrdinalIgnoreCase))
                .ToList(),

            DeliveryPointType.ParcelLocker => warehouseTypes
               .Where(x =>
                   string.Equals(
                       x.DescriptionRu?.Trim(),
                       "Почтомат",
                       StringComparison.OrdinalIgnoreCase))
               .ToList(),

            DeliveryPointType.ParcelShop => warehouseTypes
                .Where(x =>
                    string.Equals(
                        x.DescriptionRu?.Trim(),
                        "Parcel Shop",
                        StringComparison.OrdinalIgnoreCase))
                .ToList(),

            _ => throw new ArgumentOutOfRangeException(
                nameof(type), type, null)
        };

        return matches.FirstOrDefault()?.Ref
            ?? throw new InvalidOperationException(
                $"Nova Poshta warehouse type not found for {type}");
    }

}
