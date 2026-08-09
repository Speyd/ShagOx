using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
public interface IUserExistsRepository 
    : IExistsRepository<User>
{
    Task<bool> ExistsAsync(string? email, string? phone);

    Task<bool> ExistsEmailAsync(string? email);

    Task<bool> ExistsPhoneAsync(string? phone);
}