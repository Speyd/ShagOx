using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Core;
public partial class AuthController
    : ApiCookieController
{
    [HttpPost("two-factor/enable")]
    [Authorize]
    public async Task<IActionResult> EnableTwoFactor(
        CancellationToken cancellationToken)
    {
        var result = await _twoFactor
            .EnableAsync(cancellationToken);

        return result.ToActionResult();
    }

    [HttpPost("two-factor/disable")]
    [Authorize]
    public async Task<IActionResult> DisableTwoFactor(
        CancellationToken cancellationToken)
    {
        var result = await _twoFactor
            .EnableAsync(cancellationToken);

        return result.ToActionResult();
    }
}
