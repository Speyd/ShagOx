using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Interfaces.Auth.Users;

public interface IUserQueryRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByPhoneAsync(string phone);

    Task<User?> GetByContactAsync(string? email, string? phone);

    Task<List<User>> GetByCityAsync(int cityId);

    Task<List<User>> GetUsersRegisteredAfterAsync(DateTime date);

    Task<List<User>> GetUsersActiveAfterAsync(DateTime date);

    Task<List<User>> SearchByFullName(string fullName, int page, int pageSize);

    Task<List<User>> SearchByEmail(string email, int page, int pageSize);

    Task<List<User>> SearchByPhone(string phone, int page, int pageSize);
}
