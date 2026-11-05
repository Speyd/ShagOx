using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Api.Providers;
public class CurrencyProvider
    : ICurrencyProvider
{
    private readonly IUserQueryRepository _userRepository;
    private readonly IUserContext _userContext;


    public CurrencyProvider(
        IUserQueryRepository userRepository,
        IUserContext userContext)
    {
        _userRepository = userRepository;
        _userContext = userContext;
    }

    public async Task<Currency?> GetCurrencyAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var user = await _userRepository
            .GetByIdAsync(_userContext.UserId, 
                cancellationToken, status);

        return user?.Currency;
    }
}
