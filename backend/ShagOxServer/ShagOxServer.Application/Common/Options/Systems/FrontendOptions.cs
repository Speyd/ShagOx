namespace ShagOxServer.Application.Common.Options.Systems;
public sealed class FrontendOptions
{
    public const string SectionName = "Frontend";

    public string BaseUrl { get; set; } = null!;
    public string OrderDetailsPath { get; set; } = null!;
}
