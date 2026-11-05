using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
public interface IUserExistsRepository 
    : IExistsRepository<User>
{
    Task<bool> ExistsAsync(
        string? email,
        string? phone,
        string userName,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsAsync(
        string value,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByEmailAsync(
        string? email,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByPhoneAsync(
        string? phone,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
