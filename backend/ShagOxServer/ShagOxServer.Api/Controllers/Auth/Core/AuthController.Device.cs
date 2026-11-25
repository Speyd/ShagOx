using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Core;
public partial class AuthController
    : ApiCookieController
{
    [HttpPost("device/{deviceId:long}/untrust")]
    [Authorize]
    public async Task<IActionResult> UntrustDevice(
        [FromRoute] long deviceId,
        CancellationToken cancellationToken)
    {
        var result = await _trastManagement
            .UntrustDeviceAsync(deviceId, cancellationToken);

        return result.ToActionResult();
    }
}
