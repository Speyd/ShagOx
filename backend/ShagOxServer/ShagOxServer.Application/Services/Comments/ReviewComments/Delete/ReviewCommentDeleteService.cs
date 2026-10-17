using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Comments.ReviewComments.Mapping;
using ShagOxServer.Application.Services.Comments.ReviewComments.Validator;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Delete;
public class ReviewCommentDeleteService
    : IReviewCommentDeleteService
{
    private readonly IRepository<ReviewComment> _commentRepository;
    private readonly ReviewCommentValidator _commentValidator;

    private readonly ReviewCommentInvalidationService _commInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ReviewCommentDeleteService> _logger;


    public ReviewCommentDeleteService(
        IRepository<ReviewComment> commentRepository,
        ReviewCommentValidator commentValidator,
        ReviewCommentInvalidationService commInvalid,
        IUnitOfWork unitOfWork,
        ILogger<ReviewCommentDeleteService> logger)
    {
        _commentRepository = commentRepository;
        _commentValidator = commentValidator;
        _commInvalid = commInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var comment = await _commentValidator
            .GetByIdAsync(id);

        if (!comment.IsSuccess)
            return Result<DeleteResponse>.Fail(comment.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _commentRepository.Delete(comment.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete review comment. Id: {Id}",
                id);

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.ReviewCommentDeleteFailed);
        }

        await _commInvalid.InvalidateDeleteAsync(
            ReviewCommentCacheMapper.ToInfo(comment.Value!));

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               comment.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}
