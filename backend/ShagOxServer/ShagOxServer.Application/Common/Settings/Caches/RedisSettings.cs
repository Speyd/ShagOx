namespace ShagOxServer.Application.Common.Settings.Caches;
public sealed class RedisSettings
{
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