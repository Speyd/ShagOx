using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Users.Update;
using ShagOxServer.Application.Interfaces.Users.Update;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth;


namespace ShagOxServer.Application.Services.Users.Update;

public class UserUpdateService : IUserUpdateService
{
    private readonly IUserRepository _repository;

    public UserUpdateService(
        IUserRepository userRepository)
    {
        _repository = userRepository;
    }

    public async Task<Result<UserUpdateResponse>> UpdateUserAsync(
        int userId,
        UserUpdateRequest request)
    {
        var user = await _repository.GetByIdAsync(userId);

        if (user is null)
            return Result<UserUpdateResponse>.NotFound("User");

        if (request.Phone is not null)
        {
            var exists = await _repository.ExistsPhoneAsync(request.Phone);
            if (exists)
                return Result<UserUpdateResponse>.AlreadyExists("Phone");
        }

        if (request.Email is not null)
        {
            var exists = await _repository.ExistsEmailAsync(request.Email);
            if (exists)
                return Result<UserUpdateResponse>.AlreadyExists("Email");
        }

        var updatedCount = ApplyUpdates(user, request);

        if (updatedCount == 0)
            return Result<UserUpdateResponse>.Fail("No fields to update");

        await _repository.UpdateAsync(user);

        return Result<UserUpdateResponse>.Success(
            new UserUpdateResponse(DateTime.UtcNow, updatedCount)
        );
    }

    private static int ApplyUpdates(User user, UserUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Surname is not null)
        {
            user.Surname = request.Surname;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            user.Name = request.Name;
            countUpdated++;
        }

        if (request.Phone is not null)
        {
            user.Phone = request.Phone;
            countUpdated++;
        }

        if (request.Email is not null)
        {
            user.Email = request.Email;
            countUpdated++;
        }

        if (request.Avatar is not null)
        {
            user.Avatar = request.Avatar;
            countUpdated++;
        }

        if (request.CityId is not null)
        {
            user.CityId = request.CityId.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}