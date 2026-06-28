using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Interfaces.Auth;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);

    Task<User?> GetByContactAsync(string? email, string? phone);

    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByPhoneAsync(string phone);

    Task<List<User>> GetByCityAsync(int cityId);

    Task<List<User>> GetUsersRegisteredAfterAsync(DateTime date);

    Task<List<User>> GetUsersActiveAfterAsync(DateTime date);


    Task<bool> ExistsAsync(string? email, string? phone);

    Task<bool> ExistsAsync(int id);

    Task<bool> ExistsEmailAsync(string? email);

    Task<bool> ExistsPhoneAsync(string? phone);


    Task AddAsync(User user);

    Task<bool> UpdateAsync(User user);

    Task DeleteAsync(User user);
}
