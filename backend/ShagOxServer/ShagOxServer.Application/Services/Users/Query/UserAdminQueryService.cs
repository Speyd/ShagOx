using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Auth;

namespace ShagOxServer.Application.Services.Users.Query;
public class UserAdminQueryService : IUserAdminQueryService
{
    private readonly IUserRepository _repository;

    public UserAdminQueryService(
        IUserRepository userRepository)
    {
        _repository = userRepository;
    }

    public async Task<Result<UserDto>> GetByContactAsync(string? email, string? phone)
    {
        var user = await _repository.GetByContactAsync(email, phone);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<UserDto>> GetByEmailAsync(string email)
    {
        var user = await _repository.GetByEmailAsync(email);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<UserDto>> GetByPhoneAsync(string phone)
    {
        var user = await _repository.GetByEmailAsync(phone);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetByCityAsync(int cityId)
    {
        var users = await _repository.GetByCityAsync(cityId);

        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetUsersActiveAfterAsync(DateTime date)
    {
        var users = await _repository.GetUsersActiveAfterAsync(date);

        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetUsersRegisteredAfterAsync(DateTime date)
    {
        var users = await _repository.GetUsersRegisteredAfterAsync(date);

        return users.ToResultList(UserMapper.ToDto);
    }
}