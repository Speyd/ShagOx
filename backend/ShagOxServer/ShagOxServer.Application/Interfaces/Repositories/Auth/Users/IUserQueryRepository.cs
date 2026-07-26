using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
public interface IUserQueryRepository
{
    Task<User?> GetByIdAsync(int id);

    Task<List<User>> GetPagedAsync(
        PaginationParams pagination);

    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByPhoneAsync(string phone);

    Task<User?> GetByContactAsync(
        string? email,
        string? phone);

    Task<List<User>> GetByCityAsync(
        int cityId,
        PaginationParams pagination);

    Task<List<User>> GetUsersRegisteredAfterAsync(
        DateTime date,
        PaginationParams pagination);

    Task<List<User>> GetUsersActiveAfterAsync(
        DateTime date,
        PaginationParams pagination);

    Task<List<User>> Search(
        UserSearchFilter filter,
        PaginationParams pagination);
}