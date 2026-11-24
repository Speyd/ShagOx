namespace ShagOxServer.Application.DTOs.Auth.Users.UserDevices;
public record UserDeviceAuthenticationResponse
(
    long DeviceId,
    bool IsTrusted
);
