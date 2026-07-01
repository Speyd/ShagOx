namespace ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
public interface IRoleExistsRepository
{
    Task<bool> ExistsAsync(int id);

    Task<bool> ExistsAsync(string name);
}
