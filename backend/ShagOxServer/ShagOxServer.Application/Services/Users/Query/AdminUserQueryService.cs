using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Infrastructure.Interfaces.Auth;

namespace ShagOxServer.Application.Services.Users.Query;
public class AdminUserQueryService : IAdminUserQueryService
{
    private readonly IUserRepository _repository;

    public AdminUserQueryService(
        IUserRepository userRepository)
    {
        _repository = userRepository;
    }

    public Task<Result<List<UserDto>>> GetByCityAsync(int cityId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<UserDto>> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Result<UserDto>> GetByPhoneAsync(string phone)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<UserDto>>> GetByRoleAsync(int roleId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<UserDto>>> GetUsersActiveAfterAsync(DateTime date)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<UserDto>>> GetUsersRegisteredAfterAsync(DateTime date)
    {
        throw new NotImplementedException();
    }
}