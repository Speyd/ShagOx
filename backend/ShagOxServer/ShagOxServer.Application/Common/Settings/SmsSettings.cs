namespace ShagOxServer.Application.Common.Settings;
public sealed class SmsSettings
{
    public string ProductName { get; set; } = null!;
    public string AccountSID { get; set; } = null!;
    public string AuthToken { get; set; } = null!;
    public string FromPhoneNumber { get; set; } = null!;

}