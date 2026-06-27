using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Infrastructure.Interfaces.Auth;

namespace ShagOxServer.Application.Services.Users.Query;
public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _repository;

    public UserQueryService(
        IUserRepository userRepository)
    {
        _repository = userRepository;
    }

    public async Task<Result<UserDto>> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
            Result<UserDto>.Fail("User not found");

    }

    public Task<Result<UserDto>> GetMyProfileAsync()
    {
        throw new NotImplementedException();
    }
}
