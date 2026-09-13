using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
public interface IUserExistsRepository 
    : IExistsRepository<User>
{
    Task<bool> ExistsAsync(
        string? email,
        string? phone,
        string userName);

    Task<bool> ExistsByUserNameAsync(
        string userName);

    Task<bool> ExistsByEmailAsync(
        string? email);

    Task<bool> ExistsByPhoneAsync(
        string? phone);
}