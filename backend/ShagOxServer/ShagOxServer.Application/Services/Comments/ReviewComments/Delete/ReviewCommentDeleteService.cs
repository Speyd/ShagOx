using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Delete;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Comments.Core.Mapping;
using ShagOxServer.Application.Services.Comments.ReviewComments.Validator;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Delete;
public class ReviewCommentDeleteService
    : BaseDeleteService<ReviewComment>
    , IReviewCommentDeleteService
{
    private readonly IRepository<ReviewComment> _commentRepository;
    private readonly ReviewCommentValidator _commentValidator;

    private readonly ReviewCommentInvalidationService _commInvalid;

    private readonly IAdvertisementImageDeleteService _imageDeleteService;
    private readonly IPictureLoaderService _loaderService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ReviewCommentDeleteService> _logger;


    public ReviewCommentDeleteService(
        IRepository<ReviewComment> commentRepository,
        ReviewCommentValidator commentValidator,
        ReviewCommentInvalidationService commInvalid,
        IAdvertisementImageDeleteService imageDeleteService,
        IPictureLoaderService loaderService,
        IUnitOfWork unitOfWork,
        ILogger<ReviewCommentDeleteService> logger
    )
        : base(commentValidator, unitOfWork, logger)
    {
        _commentRepository = commentRepository;
        _commentValidator = commentValidator;
        _commInvalid = commInvalid;
        _imageDeleteService = imageDeleteService;
        _loaderService = loaderService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var comment = await _commentValidator
            .GetByIdAsync(id, cancellationToken);

        if (!comment.IsSuccess)
            return Result<DeleteResponse>.Fail(comment.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {

            _commentRepository.Delete(comment.Value!);

            foreach (var image in comment.Value!.Pictures)
            {
                await _imageDeleteService
                    .DeleteRecordAsync(image.Id, cancellationToken);
            }

            await _unitOfWork.CommitAsync();

            foreach (var image in comment.Value!.Pictures)
            {
                await _loaderService
                    .DeleteAsync(image.PublicId);
            }
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

        await InvalidateCache(comment.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               comment.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public override async Task InvalidateCache(
        ReviewComment entity)
    {
        await _commInvalid.InvalidateDeleteAsync(
            CommentCacheMapper.ToInfo(entity));
    }
}
