using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Interfaces.Auth.Users;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);

    Task AddAsync(User user);

    Task<bool> UpdateAsync(User user);

    Task DeleteAsync(User user);
}