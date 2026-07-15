using ShagOxServer.Application.DTOs.Users.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Users.Delete;
using ShagOxServer.Application.Services.Users.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Users.Delete;
public class UserDeleteService : IUserDeleteService
{
    private readonly IUserRepository _userRepository;
    private readonly UserValidator _userValidator;

    private readonly IUnitOfWork _unitOfWork;


    public UserDeleteService(
        IUserRepository userRepository,
        UserValidator userValidator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDeleteResponse>> DeleteAsync(int id)
    {
        var user = await _userValidator.GetByIdAsync(id);
        if (!user.IsSuccess)
            return Result<UserDeleteResponse>.Fail(user.Error ?? "");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _userRepository.Add(user.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UserDeleteResponse>.Success(
           new UserDeleteResponse(
               user.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}