using System.Text.Json.Serialization;

namespace ShagOxServer.Application.DTOs.Common.Currencies.Fx;
public class FxApiResponse
{
    [JsonPropertyName("meta")]
    public FxApiMeta Meta { get; set; } = null!;

    [JsonPropertyName("data")]
    public Dictionary<string, FxApiRate> Data { get; set; } = [];
}
