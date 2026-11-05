using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Api.Providers;
public class CurrencyProvider
    : ICurrencyProvider
{
    private readonly ICurrencyQueryService _currencyService;
    private readonly IUserContext _userContext;


    public CurrencyProvider(
        ICurrencyQueryService currencyService,
        IUserContext userContext)
    {
        _currencyService = currencyService;
        _userContext = userContext;
    }

    public async Task<Currency?> GetCurrencyAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var result = await _currencyService
            .GetByUserAsync(_userContext.UserId, 
                cancellationToken, status);

        return result.Value;
    }
}
