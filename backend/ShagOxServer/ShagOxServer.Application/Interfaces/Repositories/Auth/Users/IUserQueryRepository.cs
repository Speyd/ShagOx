using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
public interface IUserQueryRepository
{
    Task<User?> GetByIdAsync(int id);

    Task<PagedResult<User>> GetPagedAsync(
        PaginationParams pagination);

    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByPhoneAsync(string phone);

    Task<User?> GetByContactAsync(
        string? email,
        string? phone);

    Task<PagedResult<User>> Search(
        UserSearchFilter filter,
        PaginationParams pagination);

    Task<PagedResult<User>> AdminSearch(
        UserAdminSearchFilter filter,
        PaginationParams pagination);
}