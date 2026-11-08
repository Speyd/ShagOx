using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Validator;
public static class ReviewGrade
{
    public const int Min = 1;
    public const int Max = 5;

    public static Result<bool> Validate(int grade)
    {
        if (grade is < Min or > Max)
        {
            return Result<bool>.Fail(
                string.Format(
                    ValidationResources.ReviewGradeOutOfRange,
                    Min,
                    Max));
        }

        return Result<bool>.Success(true);
    }
}
