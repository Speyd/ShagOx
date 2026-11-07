using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.Interfaces.Services.Base.Update;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Delete;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewComments.Admin;

[ApiController]
[Route("api/admin/review-comment")]
[Authorize(Roles = "Admin")]
public class ReviewCommentAdminCommandsController
    : ApiController
{
    private readonly IReviewCommentDeleteService _deleteService;
    private readonly IReviewCommentUpdateService _updateService;



    public ReviewCommentAdminCommandsController(
        IReviewCommentDeleteService deleteService,
        IReviewCommentUpdateService updateService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _deleteService = deleteService;
        _updateService = updateService;
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(
        [FromRoute] long id)
    {
        var result = await _deleteService
            .DeleteAsync(id);

        return result.ToActionResult();
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
