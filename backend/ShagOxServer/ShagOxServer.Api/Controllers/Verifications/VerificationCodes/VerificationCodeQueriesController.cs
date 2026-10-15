using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Verifications;
using ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;

namespace ShagOxServer.Api.Controllers.Verifications.VerificationCodes;

[ApiController]
[Route("api/admin/verification-codes")]
[Authorize(Roles = "Admin")]
public class VerificationCodeQueriesController
    : BaseQueryController<VerificationCodeDto,
        VerificationCodeSearchFilter>
{
    public VerificationCodeQueriesController(
        IVerificationCodeQueryService queryService
    )
        : base(queryService)
    {
    }
}
