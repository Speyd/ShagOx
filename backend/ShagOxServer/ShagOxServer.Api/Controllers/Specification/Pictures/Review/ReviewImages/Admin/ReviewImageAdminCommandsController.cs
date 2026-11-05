using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.Review.ReviewImages.Admin;

[ApiController]
[Route("api/admin/review-images")]
[Authorize(Roles = "Admin")]
public class ReviewImageAdminCommandsController
    : ApiController
{
    private readonly IReviewImageUpdateService _updateService;


    public ReviewImageAdminCommandsController(
        IReviewImageUpdateService updateService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _updateService = updateService;
    }


    [HttpPatch("{id:long}/status")]
    public async Task<IActionResult> ChangeStatus(
        [FromRoute] long id,
        [FromBody] ChangeStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _updateService
            .ChangeStatusAsync(id, request,
                cancellationToken);

        return result.ToActionResult();
    }
}
