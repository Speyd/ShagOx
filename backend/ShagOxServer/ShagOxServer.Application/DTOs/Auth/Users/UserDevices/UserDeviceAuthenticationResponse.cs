namespace ShagOxServer.Application.DTOs.Auth.Users.UserDevices;
public record UserDeviceAuthenticationResponse
(
    long UserDeviceId,
    bool IsTrusted
);
