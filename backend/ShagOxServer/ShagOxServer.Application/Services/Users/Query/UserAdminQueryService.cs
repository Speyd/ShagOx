using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Users.Query;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.Domain.Filters.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Users.Query;
public class UserAdminQueryService : IUserAdminQueryService
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserExistsRepository _userExistsRepository;


    public UserAdminQueryService(
        IUserQueryRepository userRepository,
        IUserExistsRepository userExistsRepository)
    {
        _userQueryRepository = userRepository;
        _userExistsRepository = userExistsRepository;
    }


    public async Task<Result<List<UserDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var users = await _userQueryRepository
            .GetPagedAsync(pagination);

        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> Search(
       UserAdminSearchFilter filter,
       PaginationParams pagination)
    {
        var users = await _userQueryRepository
            .AdminSearch(filter, pagination);

        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        var result = await _userExistsRepository
            .ExistsByIdAsync(id);

        return result;
    }
}