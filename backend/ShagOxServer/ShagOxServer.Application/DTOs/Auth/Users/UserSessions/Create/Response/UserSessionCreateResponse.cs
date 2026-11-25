using ShagOxServer.Application.DTOs.Base.Responses;

namespace ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Create.Response;
public record UserSessionCreateResponse
(
    long Id,
    DateTime CreatedAt,
    string RefreshToken,
    DateTime ExpiresAt
) : CreateResponse(Id, CreatedAt);
