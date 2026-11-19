using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Payments.MonoBank;
[ApiController]
[Route("api/payments/monobank")]
public class MonoBankController : ControllerBase
{
    private readonly IPaymentProvider _paymentProvider;


    public MonoBankController(
        IPaymentProvider paymentProvider)
    {
        _paymentProvider = paymentProvider;
    }


    [HttpPost("webhook")]
    [AllowAnonymous]
    public async Task<IActionResult> WebHook(
        CancellationToken cancellationToken = default)
    {
        var request = HttpContext.Request;

        var result = await _paymentProvider
            .WebHook(request, cancellationToken);

        return result.ToActionResult();
    }
}
