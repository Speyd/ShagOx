using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Comments.Core.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Create;
public class ReviewCommentCreateService
    : IReviewCommentCreateService
{
    private readonly IRepository<ReviewComment> _commentRepository;
    private readonly CommentValidator<Advertisement> _commentValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ReviewCommentCreateService> _logger;


    public ReviewCommentCreateService(
        IRepository<ReviewComment> commentRepository,
        CommentValidator<Advertisement> commentValidator,
        IUnitOfWork unitOfWork,
        ILogger<ReviewCommentCreateService> logger)
    {
        _commentRepository = commentRepository;
        _commentValidator = commentValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        ReviewCommentCreateRequest request,
        long ownerId)
    {
        var exists = await _commentValidator
            .ValidateCommentAsync(
                ownerId, 
                request.CommentableEntityId
        );

        if (!exists.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(exists.Error);
        }

        var comment = ReviewCommentCreater
            .Create(ownerId, request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _commentRepository.Add(comment);

            await _unitOfWork.SaveChangesAsync();

            //await _basketInvalidation.InvalidateCreateAsync(
            //    BasketCacheMapper.ToInfo(basket));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create review comment. UserId: {UserId}",
                ownerId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.ReviewCommentCreateFailed);
        }

        return Result<CreateResponse>.Success(
           new CreateResponse(
               comment.Id,
               DateTime.UtcNow));
    }
}
