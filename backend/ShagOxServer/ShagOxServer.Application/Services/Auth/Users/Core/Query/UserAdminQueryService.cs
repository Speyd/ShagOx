using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.Users.Core.Query;
public class UserAdminQueryService 
    : IUserAdminQueryService
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserQueryService _userQueryService;
    private readonly IUserExistsRepository _userExistsRepository;


    public UserAdminQueryService(
        IUserQueryRepository userRepository,
        IUserQueryService userQueryService,
        IUserExistsRepository userExistsRepository)
    {
        _userQueryRepository = userRepository;
        _userQueryService = userQueryService;
        _userExistsRepository = userExistsRepository;
    }


    public async Task<Result<PagedResult<UserDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var users = await _userQueryRepository
            .GetPagedAsync(pagination);

        return await users.ToResultPagedAsync(
            _userQueryService.ApplyMapperAsync);
    }

    public async Task<Result<PagedResult<UserDto>>> Search(
       UserAdminSearchFilter filter,
       PaginationParams pagination)
    {
        var users = await _userQueryRepository
            .AdminSearch(filter, pagination);

        return await users.ToResultPagedAsync(
            _userQueryService.ApplyMapperAsync);
    }

    public async Task<bool> ExistsByIdAsync(
        long id)
    {
        var result = await _userExistsRepository
            .ExistsByIdAsync(id);

        return result;
    }
}