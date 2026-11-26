namespace ShagOxServer.Application.Interfaces.Services.Common.Context;
public interface IUserContext
{
    long? UserId { get; }
    string? DeviceIdentifier { get; }  
    string? RefreshToken { get; }
}
