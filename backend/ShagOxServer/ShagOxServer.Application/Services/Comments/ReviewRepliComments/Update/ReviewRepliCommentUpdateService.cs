using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Comments.Core.Mapping;
using ShagOxServer.Application.Services.Comments.ReviewRepliComments.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.PictureOrdering;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Comments.ReviewRepliComments.Update;
public class ReviewRepliCommentUpdateService
    : IReviewRepliCommentUpdateService
{
    private readonly IRepository<ReviewRepliComment> _commitRepository;
    private readonly ReviewRepliCommentValidator _commitValidator;

    private readonly ReviewRepliCommentInvalidationService _commInvalid;


    private readonly PictureOrderSyncExecutor<ReviewRepliComment,
        ReviewRepliImage> _executor;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ReviewRepliCommentUpdateService> _logger;


    public ReviewRepliCommentUpdateService(
        IRepository<ReviewRepliComment> commitRepository,
        ReviewRepliCommentValidator commitValidator,
        ReviewRepliCommentInvalidationService commInvalid,
        PictureOrderSyncExecutor<ReviewRepliComment,
            ReviewRepliImage> executor,
        IUnitOfWork unitOfWork,
        ILogger<ReviewRepliCommentUpdateService> logger)
    {
        _commitRepository = commitRepository;
        _commitValidator = commitValidator;
        _commInvalid = commInvalid;
        _executor = executor;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long commentId,
        ReviewRepliCommentUpdateRequest request)
    {
        var comment = await _commitValidator
            .GetByIdAsync(commentId);
        if (!comment.IsSuccess)
            return Result<UpdateResponse>.Fail(comment.Error);

        var updatedCount = ReviewRepliCommentUpdater
            .ApplyUpdates(comment.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _commitRepository.Update(comment.Value!);

            var imagesResult = await _executor.SyncAsync(
                comment.Value!,
                request.Pictures
            );

            if (!imagesResult.IsSuccess)
            {
                return Result<UpdateResponse>
                    .Fail(imagesResult.Error!);
            }


            await _executor.CommitAndDeleteAsync(
                imagesResult.Value!
            );
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogWarning(
                ex,
                "Concurrency conflict while updating review repli comment." +
                " Id: {Id}",
                commentId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.ConcurrencyConflictMessage);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update review repli comment. Id: {Id}",
                commentId);

            return Result<UpdateResponse>
                .Fail(EntityErrorResources.ReviewRepliCommentUpdateFailed);
        }

        await _commInvalid.InvalidateUpdateAsync(
            CommentCacheMapper.ToInfo(comment.Value!));

        return Result<UpdateResponse>.Success(result);
    }
}
