using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Comments.ReviewComments.Mapping;
using ShagOxServer.Application.Services.Comments.ReviewComments.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.PictureOrdering;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Update;
public class ReviewCommentUpdateService
    : IReviewCommentUpdateService
{
    private readonly IRepository<ReviewComment> _commitRepository;
    private readonly ReviewCommentValidator _commitValidator;

    private readonly ReviewCommentInvalidationService _commInvalid;


    private readonly PictureOrderSyncExecutor<ReviewComment,
        ReviewImage> _executor;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ReviewCommentUpdateService> _logger;


    public ReviewCommentUpdateService(
        IRepository<ReviewComment> commitRepository,
        ReviewCommentValidator commitValidator,
        ReviewCommentInvalidationService commInvalid,
        PictureOrderSyncExecutor<ReviewComment,
            ReviewImage> executor,
        IUnitOfWork unitOfWork,
        ILogger<ReviewCommentUpdateService> logger)
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
        ReviewCommentUpdateRequest request)
    {
        var comment = await _commitValidator
            .GetByIdAsync(commentId);
        if (!comment.IsSuccess)
            return Result<UpdateResponse>.Fail(comment.Error);

        var updatedCount = ReviewCommentUpdater
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
                "Concurrency conflict while updating review comment." +
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
                "Failed to update review comment. Id: {Id}",
                commentId);

            return Result<UpdateResponse>
                .Fail(EntityErrorResources.ReviewCommentUpdateFailed);
        }

        await _commInvalid.InvalidateUpdateAsync(
            ReviewCommentCacheMapper.ToInfo(comment.Value!));

        return Result<UpdateResponse>.Success(result);
    }
}
