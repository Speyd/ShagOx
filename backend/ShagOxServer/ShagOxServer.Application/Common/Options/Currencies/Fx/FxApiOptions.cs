namespace ShagOxServer.Application.Common.Options.Currencies.Fx;
public class FxApiOptions
{
    public const string SectionName = "FxApi";

    public string BaseUrl { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
}
