using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Verifications.VerificationEmails;
using ShagOxServer.Application.Interfaces.Services.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Verifications;

[ApiController]
[Route("api/auth")]
public class VerificationEmailController
    : ApiController
{
    private readonly IVerificationEmailService _emailService;


    public VerificationEmailController(
        IVerificationEmailService emailService)
    {
        _emailService = emailService;
    }


    [HttpPost("verify-email")]
    public async Task<IActionResult> GetById(
        [FromQuery] VerificationEmailRequest request)
    {
        var result = await _emailService
            .VerifyAsync(request);

        return result.ToActionResult();
    }
}