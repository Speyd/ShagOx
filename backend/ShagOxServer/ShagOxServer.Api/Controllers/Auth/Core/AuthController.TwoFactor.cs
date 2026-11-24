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
        var result = await _twoFactor.Enable();

        return result.ToActionResult();
    }
}
