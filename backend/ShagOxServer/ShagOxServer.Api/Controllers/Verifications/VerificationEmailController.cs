using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Verifications.VerificationEmails;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Verifications;

[ApiController]
[Route("api/verification")]
public class VerificationEmailController
    : ApiController
{
    private readonly IVerificationEmailService _emailService;


    public VerificationEmailController(
        IVerificationEmailService emailService)
    {
        _emailService = emailService;
    }


    [HttpPost("email")]
    public async Task<IActionResult> GetById(
        [FromBody] VerificationEmailRequest request)
    {
        request = new(request.UserId ?? UserId, request.Code);

        var result = await _emailService
            .VerifyAsync(request);

        return result.ToActionResult();
    }
}