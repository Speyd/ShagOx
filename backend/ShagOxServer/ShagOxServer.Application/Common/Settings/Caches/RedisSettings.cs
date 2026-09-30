namespace ShagOxServer.Application.Common.Settings.Caches;
public sealed class RedisSettings
{
    public string Endpoints { get; set; } = null!;
    public bool Ssl { get; set; } = true;
    public string CertificatePath { get; set; } = null!;
    public string KeyPath { get; set; } = null!;
    public string CaFileName { get; set; } = null!;
    public string ClientCrtFileName { get; set; } = null!;
    public string ClientKeyFileName { get; set; } = null!;
    public string RedisFileName { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
    public TimeSpan KeyExpiration { get; set; }
}