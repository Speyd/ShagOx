using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);

    void Add(User user);

    bool Update(User user);

    void Delete(User user);
}