using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Verifications;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Verifications;

[ApiController]
[Route("api/verifications")]
public class VerificationController
    : ApiController
{
    private readonly IVerificationService _verificationService;


    public VerificationController(
        IVerificationService verificationService)
    {
        _verificationService = verificationService;
    }


    [HttpPost]
    public async Task<IActionResult> Verification(
        [FromBody] VerificationRequest request)
    {
        request = new(request.UserId ?? UserId, request.Code);

        var result = await _verificationService
            .VerifyAsync(request);

        return result.ToActionResult();
    }
}