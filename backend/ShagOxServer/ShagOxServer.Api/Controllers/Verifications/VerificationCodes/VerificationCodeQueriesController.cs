using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Verifications.VerificationCodes;

[ApiController]
[Route("api/admin/verification-codes")]
[Authorize(Roles = "Admin")]
public class VerificationCodeQueriesController
    : ApiController
{
    private readonly IVerificationCodeQueryService _queryService;


    public VerificationCodeQueriesController(
        IVerificationCodeQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(
        [FromRoute] long id)
    {
        var result = await _queryService
            .GetByIdAsync(id);

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetPagedAsync(pagination);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] VerificationCodeSearchFilter filter,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .SearchAsync(filter, pagination);

        return result.ToActionResult();
    }
}