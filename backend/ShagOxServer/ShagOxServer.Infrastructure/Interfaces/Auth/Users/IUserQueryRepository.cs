using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Auth.Users;
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

    Task<List<User>> SearchByFullName(
        string fullName,
        PaginationParams pagination);

    Task<List<User>> SearchByEmail(
       string email,
       PaginationParams pagination);

    Task<List<User>> SearchByPhone(
        string phone, 
        PaginationParams pagination);
}
