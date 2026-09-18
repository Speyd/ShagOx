using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Verifications.VerificationEmails;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Verifications;

[ApiController]
[Route("api/verifications")]
public class VerificationEmailController
    : ApiController
{
    private readonly IVerificationService _verificationService;


    public VerificationEmailController(
        IVerificationService verificationService)
    {
        _verificationService = verificationService;
    }


    [HttpPost]
    public async Task<IActionResult> Verification(
        [FromBody] VerificationEmailRequest request)
    {
        request = new(request.UserId ?? UserId, request.Code);

        var result = await _verificationService
            .VerifyAsync(request);

        return result.ToActionResult();
    }
}