namespace ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
public interface IUserRoleExistsRepository
{
    Task<bool> ExistsAsync(int roleId, int userId);
}