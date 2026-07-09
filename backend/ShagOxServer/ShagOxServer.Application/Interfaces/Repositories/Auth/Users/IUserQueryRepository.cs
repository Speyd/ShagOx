using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
public interface IUserQueryRepository
{
    Task<User?> GetByIdAsync(int id);

    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByPhoneAsync(string phone);

    Task<User?> GetByContactAsync(
        string? email,
        string? phone);

    Task<List<User>> GetByCityAsync(int cityId);

    Task<List<User>> GetUsersRegisteredAfterAsync(DateTime date);

    Task<List<User>> GetUsersActiveAfterAsync(DateTime date);

    Task<List<User>> Search(
        UserSearchFilter filter,
        PaginationParams pagination);
}
