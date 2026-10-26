using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewRepliComments.Admin;

[ApiController]
[Route("api/admin/review-repli-comment")]
[Authorize(Roles = "Admin")]
public class ReviewRepliCommentAdminCommandsController
    : ApiController
{
    private readonly IReviewRepliCommentDeleteService _deleteService;


    public ReviewRepliCommentAdminCommandsController(
        IReviewRepliCommentDeleteService deleteService)
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
