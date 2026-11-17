using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.EntityNames.Extensions;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Comments.Core.Mapping;
using ShagOxServer.Application.Services.Comments.Core.Validator;
using ShagOxServer.Application.Services.Comments.ReviewRepliComments.Validator;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Comments.ReviewRepliComments.Create;

public class ReviewRepliCommentCreateService
    : IReviewRepliCommentCreateService
{
    private readonly IRepository<ReviewRepliComment> _commentRepository;
    private readonly CommentValidator<ReviewComment> _commentValidator;
    private readonly ReviewRepliCommentValidator _repliValidator;

    private readonly ReviewRepliCommentInvalidationService _commInvalid;

    private readonly IReviewRepliImageCreateService _imageCreateService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ReviewRepliCommentCreateService> _logger;


    public ReviewRepliCommentCreateService(
        IRepository<ReviewRepliComment> commentRepository,
        CommentValidator<ReviewComment> commentValidator,
        ReviewRepliCommentInvalidationService commInvalid,
        ReviewRepliCommentValidator repliValidator,
        IReviewRepliImageCreateService imageCreateService,
        IUnitOfWork unitOfWork,
        ILogger<ReviewRepliCommentCreateService> logger)
    {
        _commentRepository = commentRepository;
        _commentValidator = commentValidator;
        _commInvalid = commInvalid;
        _repliValidator = repliValidator;
        _imageCreateService = imageCreateService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        ReviewRepliCommentCreateRequest request,
        long ownerId,
        CancellationToken cancellationToken = default)
    {
        var validation = await ValidationAsync(
            request,
            ownerId,
            cancellationToken
        );

        if (!validation.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(validation.Error);
        }

        var comment = ReviewRepliCommentCreater
            .Create(ownerId, request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _commentRepository.Add(comment);

            await _unitOfWork.SaveChangesAsync();

            if (request.Pictures is { Count: > 0 })
            {
                var imagesResult = await _imageCreateService
                    .CreateAsync(
                        new PicturesCreateRequest(
                            comment.Id,
                            request.Pictures
                        ), cancellationToken
                    );

                if (!imagesResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();

                    return Result<CreateResponse>
                        .Fail(imagesResult.Error!);
                }
            }

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create review repli comment. " +
                "UserId: {UserId}",
                ownerId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.ReviewRepliCommentCreateFailed);
        }

        await _commInvalid.InvalidateCreateAsync(
             CommentCacheMapper.ToInfo(comment));

        return Result<CreateResponse>.Success(
           new CreateResponse(
               comment.Id,
               DateTime.UtcNow));
    }

    public async Task<Result<bool>> ValidationAsync(
        ReviewRepliCommentCreateRequest request,
        long ownerId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return Result<bool>
                .Fail(ValidationResources.CommentTextRequired);
        }

        var existsComment = await _commentValidator
            .ValidateCommentAsync(
                ownerId,
                request.CommentableEntityId,
                cancellationToken
        );

        if (!existsComment.IsSuccess)
        {
            return Result<bool>
                .Fail(existsComment.Error);
        }

        if (request.RepliId.HasValue)
        {
            var parent = await _repliValidator
                .GetByIdAttachAsync(request.RepliId.Value,
                    cancellationToken);

            if (!parent.IsSuccess)
            {
                return Result<bool>
                    .Fail(parent.Error);
            }

            if (parent.Value!.CommentableEntityId !=
                request.CommentableEntityId)
            {
                return Result<bool>.NotFound(
                    EntityNameExtensions
                        .GetLocalizedName<ReviewRepliComment>());
            }
        }

        return Result<bool>.Success(true);
    }
}
