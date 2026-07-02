using ShagOxServer.SharedKernel.Results;
using ShagOxServer.SharedKernel.Results.Extensions;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Auth.Users;

namespace ShagOxServer.Application.Services.Users.Query;
public class UserAdminQueryService : IUserAdminQueryService
{
    private readonly IUserQueryRepository _userRepository;
    private readonly IUserExistsRepository _userExistsRepository;

    private readonly IAdvertisementExistsRepository _advertRepository;


    public UserAdminQueryService(
        IUserQueryRepository userRepository,
        IUserExistsRepository userExistsRepository,
        IAdvertisementExistsRepository advertRepository)
    {
        _userRepository = userRepository;
        _userExistsRepository = userExistsRepository;
        _advertRepository = advertRepository;
    }

    public async Task<Result<UserDto>> GetByContactAsync(string? email, string? phone)
    {
        var user = await _userRepository.GetByContactAsync(email, phone);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<UserDto>> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<UserDto>> GetByPhoneAsync(string phone)
    {
        var user = await _userRepository.GetByEmailAsync(phone);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetByCityAsync(int cityId)
    {
        var users = await _userRepository.GetByCityAsync(cityId);

        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetUsersActiveAfterAsync(DateTime date)
    {
        var users = await _userRepository.GetUsersActiveAfterAsync(date);

        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetUsersRegisteredAfterAsync(DateTime date)
    {
        var users = await _userRepository.GetUsersRegisteredAfterAsync(date);

        return users.ToResultList(UserMapper.ToDto);
    }


    public async Task<bool> ExistsAsync(int id)
    {
        var result = await _userExistsRepository.ExistsAsync(id);

        return result;
    }

    public async Task<bool> IsAdvertisementOwnerAsync(int userId, int adId)
    {
        var result = await _advertRepository.IsOwnerAsync(adId, userId);

        return result;
    }
}