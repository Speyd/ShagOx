using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Core.Delete;
public class UserDeleteService 
    : IUserDeleteService
{
    private readonly IRepository<User> _userRepository;
    private readonly UserValidator _userValidator;

    private readonly IAvatarDeleteService _avatarDeleteService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserDeleteService> _logger;


    public UserDeleteService(
        IRepository<User> userRepository,
        UserValidator userValidator,
        IAvatarDeleteService avatarDeleteService,
        IUnitOfWork unitOfWork,
        ILogger<UserDeleteService> logger)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _avatarDeleteService = avatarDeleteService;
        _unitOfWork = unitOfWork;
        _logger = logger;
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
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete user. Id: {Id}",
                id);

            return Result<DeleteResponse>
                     .Fail("Failed to delete user.");
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               user.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}