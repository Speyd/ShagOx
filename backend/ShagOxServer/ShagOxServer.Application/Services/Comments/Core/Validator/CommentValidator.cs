using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Comments.Core.Validator;
public class CommentValidator<TCommentable>
    where TCommentable : BaseEntity
{
    private readonly UserValidator _userValidator;
    private readonly BaseValidator<TCommentable> _commentableValidator;


    public CommentValidator(
        UserValidator userValidator,
        BaseValidator<TCommentable> commentableValidator
    )
    {
        _userValidator = userValidator;
        _commentableValidator = commentableValidator;
    }


    public async Task<Result<bool>> ValidateCommentAsync(
        long userId, 
        long commentableId)
    {
        var resultUser = await _userValidator
            .ExistsByIdAsync(userId);

        if (!resultUser.IsSuccess)
            return resultUser;

        var resultCommentable = await _commentableValidator
            .ExistsByIdAsync(userId);

        if (!resultCommentable.IsSuccess)
            return resultCommentable;

        return Result<bool>.Success(true);
    }
}
