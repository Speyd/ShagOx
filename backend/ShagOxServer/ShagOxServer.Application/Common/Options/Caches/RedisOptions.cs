namespace ShagOxServer.Application.Common.Options.Caches;
public sealed class RedisOptions
{
    public const string SectionDataName = "Redis";
    public const string SectionUserName = "UserRedis";

    public string Endpoints { get; set; } = null!;
    public bool Ssl { get; set; } = true;
    public string CaPath { get; set; } = null!;
    public string CaKeyPath { get; set; } = null!;
    public string CaFileName { get; set; } = null!;
    public string ClientPath { get; set; } = null!;
    public string ClientCrtFileName { get; set; } = null!;
    public string ClientKeyFileName { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
    public TimeSpan KeyExpiration { get; set; }
}
