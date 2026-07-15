namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
public interface IRoleExistsRepository
{
    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsByNameAsync(string name);
}