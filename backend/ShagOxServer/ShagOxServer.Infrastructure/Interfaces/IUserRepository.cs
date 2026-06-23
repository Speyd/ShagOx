using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Interfaces;
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByPhoneAsync(string phone);

    Task<User?> GetByIdAsync(int id);

    Task AddAsync(User user);

    Task<bool> ExistsAsync(string? email, string? phone);

    Task<bool> ExistsEmailAsync(string? email);

    Task<bool> ExistsPhoneAsync(string? phone);

    Task<bool> UpdateAsync(User user);
}
