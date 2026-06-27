using ShagOxServer.Infrastructure.Interfaces.Auth;

namespace ShagOxServer.Application.Services.Users.Query;
public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _repository;

    public UserQueryService(
        IUserRepository userRepository)
    {
        _repository = userRepository;
    }
}
