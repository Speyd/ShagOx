namespace ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Create;
public sealed record UserSessionCreateRequest
(
    long OwnerId,
    long DeviceId
);
