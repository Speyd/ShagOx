using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Tokens.Jwt;
public interface IJwtService
{
    public string GenerateToken(
        User user,
        long sessionId);
}
