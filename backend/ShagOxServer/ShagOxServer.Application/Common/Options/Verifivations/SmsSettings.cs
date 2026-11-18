namespace ShagOxServer.Application.Common.Options.Verifivations;
public sealed class SmsOptions
{
    public string ProductName { get; set; } = null!;
    public string AccountSID { get; set; } = null!;
    public string AuthToken { get; set; } = null!;
    public string FromTwilio { get; set; } = null!;

}
