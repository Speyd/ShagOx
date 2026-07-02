using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Common.Context;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using ShagOxServer.SharedKernel.Paginations;

namespace ShagOxServer.Application.Services.Users.Query;
public class UserQueryService : IUserQueryService
{
    private readonly IUserQueryRepository _repository;
    private readonly IUserContext _context;


    public UserQueryService(
        IUserQueryRepository userRepository,
        IUserContext userContext)
    {
        _repository = userRepository;
        _context = userContext;
    }

    public async Task<Result<UserDto>> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<UserDto>> GetMyProfileAsync()
    {
        var user = await _repository.GetByIdAsync(_context.UserId);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> SearchByFullName(
       string fullName,
	   PaginationParams pagination)
    {
        var users = await _repository.SearchByFullName(
            fullName, pagination);

        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> SearchByEmail(
        string email,
		PaginationParams pagination)
    {
        var users = await _repository.SearchByEmail(
             email, pagination);


        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> SearchByPhone(
        string phone,
		PaginationParams pagination)
    {
        var users = await _repository.SearchByPhone(
            phone, pagination);


        return users.ToResultList(UserMapper.ToDto);
    }
}
