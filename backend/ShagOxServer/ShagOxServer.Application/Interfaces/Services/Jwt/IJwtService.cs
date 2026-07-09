using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Services.Jwt;
public interface IJwtService
{
    public string GenerateToken(User user);
}
