using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.Interfaces.Services.Base.Update;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Delete;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Update;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewRepliComments.Admin;

[ApiController]
[Route("api/admin/review-repli-comment")]
[Authorize(Roles = "Admin")]
public class ReviewRepliCommentAdminCommandsController
    : ApiController
{
    private readonly IReviewRepliCommentDeleteService _deleteService;
    private readonly IReviewRepliCommentUpdateService _updateService;


    public ReviewRepliCommentAdminCommandsController(
        IReviewRepliCommentDeleteService deleteService,
        IReviewRepliCommentUpdateService updateService)
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
