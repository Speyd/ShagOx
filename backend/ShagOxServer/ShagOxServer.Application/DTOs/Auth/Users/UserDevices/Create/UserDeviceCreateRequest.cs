namespace ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
public sealed record UserDeviceCreateRequest
(
    long OwnerId,
    long DeviceId
);
