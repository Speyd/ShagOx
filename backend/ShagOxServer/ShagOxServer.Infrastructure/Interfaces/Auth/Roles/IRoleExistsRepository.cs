namespace ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
internal interface IRoleExistsRepository
{
    Task<bool> ExistsAsync(int id);

    Task<bool> ExistsAsync(string name);
}
