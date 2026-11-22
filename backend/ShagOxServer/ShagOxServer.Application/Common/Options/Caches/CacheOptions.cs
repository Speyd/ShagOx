namespace ShagOxServer.Application.Common.Options.Caches;
public sealed class CacheOptions
{
    public const string SectionName = "Cache";

    public TimeSpan DefaultExpiration { get; set; }
    
    public TimeSpan CatalogExpiration { get; set; }
}
