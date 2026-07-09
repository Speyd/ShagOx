using ShagOxServer.Application.DTOs.Users.Delete;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Users.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Users.Delete;

public class UserDeleteService : IUserDeleteService
{
    private readonly IUserRepository _repository;

    public UserDeleteService(
        IUserRepository userRepository)
    {
        _repository = userRepository;
    }

    public async Task<Result<UserDeleteResponse>> DeleteUserAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
            return Result<UserDeleteResponse>.Fail("User not found");

        await _repository.DeleteAsync(user);
        return Result<UserDeleteResponse>.Success(
           new UserDeleteResponse(
               user.Id,
               DateTime.UtcNow
           )
       );
    }
}