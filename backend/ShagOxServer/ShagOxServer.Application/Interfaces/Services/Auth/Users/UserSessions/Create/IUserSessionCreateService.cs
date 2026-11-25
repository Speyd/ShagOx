using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Create;
using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Create.Response;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Create;
public interface IUserSessionCreateService
    : ICreateService<
        UserSessionCreateResponse,
        UserSessionCreateRequest
        >
{
}
