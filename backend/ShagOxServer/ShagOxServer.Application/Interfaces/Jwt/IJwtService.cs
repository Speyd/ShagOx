using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Jwt;
public interface IJwtService
{
    public string GenerateToken(User user);
}
