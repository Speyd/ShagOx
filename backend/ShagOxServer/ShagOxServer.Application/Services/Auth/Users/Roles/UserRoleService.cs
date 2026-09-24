using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Resources.Auth.Registrations;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Roles;
public class UserRoleService
{
    private readonly IRoleQueryRepository _roleQueryRepository;

    public UserRoleService(
        IRoleQueryRepository roleQueryRepository)
    {
        _roleQueryRepository = roleQueryRepository;
    }

    public async Task<Result<bool>> AddDefaultRoleAsync(
        User user)
    {
        var role =
            await _roleQueryRepository
                .GetByNameAsync(RoleNames.User);

        if (role is null)
        {
            return Result<bool>
                .Fail(RegistrationAuth.DefaultRoleNotFound);
        }

        user.UserRoles.Add(new UserRole
        {
            Role = role
        });

        return Result<bool>.Success(true);
    }
}