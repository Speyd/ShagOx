using System.Text.Json.Serialization;

namespace ShagOxServer.Application.DTOs.Common.Currencies.Fx;
public class FxApiMeta
{
    [JsonPropertyName("last_updated_at")]
    public DateTimeOffset LastUpdatedAt { get; set; }

    [JsonPropertyName("base_currency")]
    public string BaseCurrency { get; set; } = "";
}
