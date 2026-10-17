using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Create;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Update;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Create;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Delete;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Comments.ReviewComments;

[ApiController]
[Route("api/review-comment")]
[Authorize]
public class ReviewCommentCommandsController
    : ApiOwnerExistsController
{
    private readonly IReviewCommentCreateService _createService;
    private readonly IReviewCommentUpdateService _updateService;
    private readonly IReviewCommentDeleteService _deleteService;


    public ReviewCommentCommandsController(
        IReviewCommentCreateService createService,
        IReviewCommentUpdateService updateService,
        IReviewCommentDeleteService deleteService,
        IReviewCommentExistsRepository existsCommentRepository,
        IUserAdminQueryService userQueryService
    )
        : base(existsCommentRepository, userQueryService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        ReviewCommentCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request, UserId);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] ReviewCommentUpdateRequest request)
    {
        var forbidden = await CheckOwnershipAsync(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(
        [FromRoute] long id)
    {
        var forbidden = await CheckOwnershipAsync(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _deleteService
            .DeleteAsync(id);

        return result.ToActionResult();
    }
}
