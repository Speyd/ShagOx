using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Delete;
using ShagOxServer.Application.Services.Auth.Users.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Delete;
public class UserDeleteService 
    : IUserDeleteService
{
    private readonly IRepository<User> _userRepository;
    private readonly UserValidator _userValidator;

    private readonly IUnitOfWork _unitOfWork;


    public UserDeleteService(
        IRepository<User> userRepository,
        UserValidator userValidator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var user = await _userValidator.GetByIdAsync(id);
        if (!user.IsSuccess)
            return Result<DeleteResponse>.Fail(user.Error);

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

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               user.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}