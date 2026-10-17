using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Comments.ReviewComments.Validator;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Update;
public class ReviewCommentUpdateService
    : IReviewCommentUpdateService
{
    private readonly IRepository<ReviewComment> _commitRepository;
    private readonly ReviewCommentValidator _commitValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ReviewCommentUpdateService> _logger;


    public ReviewCommentUpdateService(
        IRepository<ReviewComment> commitRepository,
        ReviewCommentValidator commitValidator,
        IUnitOfWork unitOfWork,
        ILogger<ReviewCommentUpdateService> logger)
    {
        _commitRepository = commitRepository;
        _commitValidator = commitValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long commitId,
        ReviewCommentUpdateRequest request)
    {
        var commit = await _commitValidator
            .GetByIdAsync(commitId);
        if (!commit.IsSuccess)
            return Result<UpdateResponse>.Fail(commit.Error);

        var updatedCount = ReviewCommentUpdater
            .ApplyUpdates(commit.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _commitRepository.Update(commit.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogWarning(
                ex,
                "Concurrency conflict while updating review comment." +
                " Id: {Id}",
                commitId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.ConcurrencyConflictMessage);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update review comment. Id: {Id}",
                commitId);

            return Result<UpdateResponse>
                .Fail(EntityErrorResources.ReviewCommentUpdateFailed);
        }

        //await _basketInvalid.InvalidateUpdateAsync(
        //    BasketCacheMapper.ToInfo(basket.Value!));

        return Result<UpdateResponse>.Success(result);
    }
}
