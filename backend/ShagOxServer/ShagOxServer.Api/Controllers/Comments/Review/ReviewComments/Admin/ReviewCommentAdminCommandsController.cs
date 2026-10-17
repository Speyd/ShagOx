using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewComments.Admin;

[ApiController]
[Route("api/admin/review-comment")]
[Authorize(Roles = "Admin")]
public class ReviewCommentAdminCommandsController
    : ApiController
{
    private readonly IReviewCommentDeleteService _deleteService;


    public ReviewCommentAdminCommandsController(
        IReviewCommentDeleteService deleteService)
    {
        _deleteService = deleteService;
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(
        [FromRoute] long id)
    {
        var result = await _deleteService
            .DeleteAsync(id);

        return result.ToActionResult();
    }
}
