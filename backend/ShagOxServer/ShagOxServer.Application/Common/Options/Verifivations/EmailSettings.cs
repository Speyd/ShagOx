namespace ShagOxServer.Application.Common.Options.Verifivations;
public sealed class EmailOptions
{
    public string SmtpHost { get; set; } = null!;
    public int SmtpPort { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string From { get; set; } = null!;
}
