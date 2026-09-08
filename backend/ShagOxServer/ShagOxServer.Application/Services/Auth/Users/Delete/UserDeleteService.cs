using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Application.Services.Auth.Users.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Delete;
public class UserDeleteService 
    : IUserDeleteService
{
    private readonly IRepository<User> _userRepository;
    private readonly UserValidator _userValidator;

    private readonly IAvatarDeleteService _avatarDeleteService;

    private readonly IUnitOfWork _unitOfWork;


    public UserDeleteService(
        IRepository<User> userRepository,
        UserValidator userValidator,
        IAvatarDeleteService avatarDeleteService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _avatarDeleteService = avatarDeleteService;
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
            _userRepository.Delete(user.Value!);

            if (user.Value!.AvatarId is not null)
            {
                await _avatarDeleteService
                    .DeleteAsync(user.Value!.AvatarId.Value);
            }

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