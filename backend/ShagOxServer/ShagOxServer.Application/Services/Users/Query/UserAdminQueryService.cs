using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Users.Query;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Users.Query;
public class UserAdminQueryService : IUserAdminQueryService
{
    private readonly IUserQueryRepository _userRepository;
    private readonly IUserExistsRepository _userExistsRepository;


    public UserAdminQueryService(
        IUserQueryRepository userRepository,
        IUserExistsRepository userExistsRepository)
    {
        _userRepository = userRepository;
        _userExistsRepository = userExistsRepository;
    }


    public async Task<Result<UserDto>> GetByContactAsync(string? email, string? phone)
    {
        var user = await _userRepository
            .GetByContactAsync(email, phone);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<UserDto>> GetByEmailAsync(string email)
    {
        var user = await _userRepository
            .GetByEmailAsync(email);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<UserDto>> GetByPhoneAsync(string phone)
    {
        var user = await _userRepository
            .GetByEmailAsync(phone);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetByCityAsync(
        int cityId,
        PaginationParams pagination)
    {
        var users = await _userRepository
            .GetByCityAsync(cityId, pagination);

        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetUsersActiveAfterAsync(
        DateTime date,
        PaginationParams pagination)
    {
        var users = await _userRepository
            .GetUsersActiveAfterAsync(date, pagination);

        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetUsersRegisteredAfterAsync(
        DateTime date,
        PaginationParams pagination)
    {
        var users = await _userRepository
            .GetUsersRegisteredAfterAsync(date, pagination);

        return users.ToResultList(UserMapper.ToDto);
    }


    public async Task<bool> ExistsByIdAsync(int id)
    {
        var result = await _userExistsRepository
            .ExistsByIdAsync(id);

        return result;
    }
}