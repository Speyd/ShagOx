using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Register;
public partial class RegisterService
    : IRegisterService
{
    public async Task<Result<bool>> CreateUserAggregateAsync(
        User user)
    {
        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync();

        var basketResult = await _basketService.CreateInternalAsync(
            new BasketCreateRequest(),
            user.Id);


        if (!basketResult.IsSuccess)
        {
            return Result<bool>
                .Fail(basketResult.Error);
        }

        var roleResult = await _roleService
            .AddDefaultRoleAsync(user);

        if (!roleResult.IsSuccess)
        {
            return Result<bool>
                .Fail(roleResult.Error);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}
