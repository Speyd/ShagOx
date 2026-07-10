using ShagOxServer.Application.DTOs.Users.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Users.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Users.Delete;
public class UserDeleteService : IUserDeleteService
{
    private readonly IUserRepository _repository;

    private readonly IUnitOfWork _unitOfWork;

    public UserDeleteService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDeleteResponse>> DeleteUserAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
            return Result<UserDeleteResponse>.Fail("User not found");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Add(user);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UserDeleteResponse>.Success(
           new UserDeleteResponse(
               user.Id,
               DateTime.UtcNow
           )
       );
    }
}