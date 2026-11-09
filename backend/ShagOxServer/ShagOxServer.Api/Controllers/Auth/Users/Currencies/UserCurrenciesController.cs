using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users.Currencies;
[ApiController]
[Route("api/users")]
public class UserCurrenciesController
    : ApiController
{
    private readonly IUserQueryRepository _userService;
    private readonly ICurrencyProvider _currencyProvider;



    public UserCurrenciesController(
        IUserQueryRepository userService,
        ICurrencyProvider currencyProvider,
        IUserContext userContext
    )
        : base(userContext)
    {
        _userService = userService;
        _currencyProvider = currencyProvider;
    }


    [Authorize]
    [HttpGet("me/currency")]
    public async Task<IActionResult> GetMyRole(
        CancellationToken cancellationToken)
    {
        var currency = await _currencyProvider
            .GetCurrencyAsync(cancellationToken);

        if (currency is null)
        {
            return Result<bool>
                .NotFound(EntityNamesResources.User)
                .ToActionResult();
        }

        return Ok(currency);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{userId:long}/currency")]
    public async Task<IActionResult> GetUserCurrency(
        [FromRoute] long userId,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var user = await _userService
            .GetByIdIncludeAsync(userId,
                cancellationToken, status);

        if (user is null)
        {
            return Result<bool>
                .NotFound(EntityNamesResources.User)
                .ToActionResult();
        }

        return Ok(user.Currency);
    }
}
