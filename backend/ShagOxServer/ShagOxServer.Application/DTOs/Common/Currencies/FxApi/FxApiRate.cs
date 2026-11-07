using System.Text.Json.Serialization;

namespace ShagOxServer.Application.DTOs.Common.Currencies.Fx;
public class FxApiRate
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = "";

    [JsonPropertyName("value")]
    public decimal Value { get; set; }
}
