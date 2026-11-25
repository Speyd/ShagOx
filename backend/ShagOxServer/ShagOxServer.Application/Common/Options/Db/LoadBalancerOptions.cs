namespace ShagOxServer.Application.Common.Options.Db;
public sealed class LoadBalancerOptions
{
    public const string SectionName = "LoadBalancer";

    public TimeSpan MaxHealthAge { get; init; }
}
