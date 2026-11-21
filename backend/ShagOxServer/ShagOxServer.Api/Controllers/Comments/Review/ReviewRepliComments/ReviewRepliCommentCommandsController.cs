using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Create;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Update;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Create;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Delete;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewRepliComments;

[ApiController]
[Route("api/review-repli-comment")]
[Authorize]
public class ReviewRepliCommentCommandsController
    : ApiOwnerExistsController
{
    private readonly IReviewRepliCommentCreateService _createService;
    private readonly IReviewRepliCommentUpdateService _updateService;
    private readonly IReviewRepliCommentDeleteService _deleteService;


    public ReviewRepliCommentCommandsController(
        IReviewRepliCommentCreateService createService,
        IReviewRepliCommentUpdateService updateService,
        IReviewRepliCommentDeleteService deleteService,
        IReviewRepliCommentExistsRepository existsCommentRepository,
        IUserAdminQueryService userQueryService,
        IUserContext userContext
    )
        : base(existsCommentRepository, userQueryService, userContext)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromForm] ReviewRepliCommentCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request, UserId!.Value);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] ReviewRepliCommentUpdateRequest request)
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
            .SoftDeleteAsync(id);

        return result.ToActionResult();
    }
}
