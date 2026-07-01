using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Common.Context;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Auth.Users;

namespace ShagOxServer.Application.Services.Users.Query;
public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _repository;
    private readonly IUserQueryRepository _queryRepository;
    private readonly IUserContext _context;


    public UserQueryService(
        IUserRepository userRepository,
        IUserQueryRepository userQueryRepository,
        IUserContext userContext)
    {
        _repository = userRepository;
        _queryRepository = userQueryRepository;
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
       int page,
       int pageSize)
    {
        var users = await _queryRepository.SearchByFullName(
            fullName, page, pageSize);

        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> SearchByEmail(
        string email,
        int page,
        int pageSize)
    {
        var users = await _queryRepository.SearchByEmail(
             email, page, pageSize);


        return users.ToResultList(UserMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> SearchByPhone(
        string phone,
        int page,
        int pageSize)
    {
        var users = await _queryRepository.SearchByPhone(
            phone, page, pageSize);


        return users.ToResultList(UserMapper.ToDto);
    }
}
