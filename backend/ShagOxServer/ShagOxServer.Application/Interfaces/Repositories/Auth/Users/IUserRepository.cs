using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);

    Task AddAsync(User user);

    Task<bool> UpdateAsync(User user);

    Task DeleteAsync(User user);
}