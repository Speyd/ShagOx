using ShagOxServer.Domain.Entities.Auth;

namespace ShagOxServer.Application.Interfaces.Services.Jwt;
public interface IJwtService
{
    public string GenerateToken(
        User user);
}
