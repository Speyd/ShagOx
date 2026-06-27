using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Common.Context;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Auth;

namespace ShagOxServer.Application.Services.Users.Query;
public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _repository;
    private readonly IUserContext _context;


    public UserQueryService(
        IUserRepository userRepository,
        IUserContext userContext)
    {
        _repository = userRepository;
        _context = userContext;
    }

    public async Task<Result<UserDto>> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<UserDto>> GetMyProfileAsync()
    {
        var user = await _repository.GetByIdAsync(_context.UserId);

        return user.ToResult(UserMapper.ToDto);
    }
}
