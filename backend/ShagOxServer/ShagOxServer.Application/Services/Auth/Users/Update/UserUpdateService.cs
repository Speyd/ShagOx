using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Auth.Users.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Application.Services.Auth.Users.Validator;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Update;
public class UserUpdateService 
    : IUserUpdateService
{
    private readonly IRepository<User> _userRepository;
    private readonly UserValidator _userValidator;

    private readonly CityValidator _cityValidator;

    private readonly IAvatarDeleteService _avatarDeleteService;
    private readonly IAvatarCreateService _avatarCreateService;

    private readonly IUnitOfWork _unitOfWork;


    public UserUpdateService(
        IRepository<User> userRepository,
        UserValidator userValidator,
        CityValidator cityValidator,
        IAvatarDeleteService avatarDeleteService,
        IAvatarCreateService avatarCreateService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _cityValidator = cityValidator;
        _avatarDeleteService = avatarDeleteService;
        _avatarCreateService = avatarCreateService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int userId,
        UserUpdateRequest request)
    {
        var user = await _userValidator
            .GetByIdWithIncludesAsync(userId);

        if (!user.IsSuccess)
            return Result<UpdateResponse>.Fail(user.Error);


        var validation = await
            ValidateUpdatesAsync(user.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = UserUpdater
            .ApplyUpdates(user.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _userRepository.Update(user.Value!);

            if(request.Avatar is not null)
                await UpdateAvatarAsync(user.Value!, request.Avatar);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        User user,
        UserUpdateRequest request)
    {
        if (request.CityId.HasValue &&
           request.CityId != user.CityId)
        {
            var city = await _cityValidator
                .ExistsByIdAsync(request.CityId.Value);

            if (!city.IsSuccess)
                return Result<bool>.Fail(city.Error);
        }

        if (request.Phone is not null &&
            request.Phone != user.Phone)
        {
            var phone = await _userValidator
                .NotExistsByPhoneAsync(request.Phone);

            if (!phone.IsSuccess)
                return Result<bool>.Fail(phone.Error);
        }

        if (request.Email is not null &&
            request.Email != user.Email)
        {
            var email = await _userValidator
                .NotExistsByEmailAsync(request.Email);

            if (!email.IsSuccess)
                return Result<bool>.Fail(email.Error);
        }

        return Result<bool>.Success(true);
    }

    private async Task UpdateAvatarAsync(
        User user,
        IFormFile avatarFile)
    {
        var oldAvatar = user.Avatar;

        var result = await _avatarCreateService.CreateAsync(
            new AvatarCreateRequest(
                avatarFile,
                user.Id));

        if (!result.IsSuccess)
            throw new Exception(result.Error?.ToString());

        if (oldAvatar is not null)
        {
            await _avatarDeleteService.DeleteAsync(oldAvatar.Id);
        }
    }
}