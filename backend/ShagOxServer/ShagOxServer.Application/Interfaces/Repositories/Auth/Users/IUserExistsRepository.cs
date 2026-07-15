namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
public interface IUserExistsRepository
{
    Task<bool> ExistsAsync(string? email, string? phone);

    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsEmailAsync(string? email);

    Task<bool> ExistsPhoneAsync(string? phone);
}
