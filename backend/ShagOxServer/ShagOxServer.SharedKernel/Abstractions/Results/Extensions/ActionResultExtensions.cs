using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.SharedKernel.Abstractions.Results.Enum;

namespace ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
public static class ActionResultExtensions
{
    public static IActionResult ToActionResult<T>(
    this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return result.ErrorType switch
        {
            ResultErrorType.NotFound =>
                new NotFoundObjectResult(result.Error),

            ResultErrorType.Unauthorized =>
                new ObjectResult(result.Error)
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                },

            ResultErrorType.Forbidden =>
                new ObjectResult(result.Error)
                {
                    StatusCode = StatusCodes.Status403Forbidden
                },

            ResultErrorType.InternalServer =>
                new ObjectResult(result.Error)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                },

            ResultErrorType.Conflict =>
                new ConflictObjectResult(result.Error),

            _ =>
                new BadRequestObjectResult(result.Error)
        };
    }
    //public static IActionResult ToActionListResult<T>(
    //    this Result<IEnumerable<T>> result)
    //{
    //    if (result.IsSuccess)
    //        return new OkObjectResult(result.Value);

    //    return new BadRequestObjectResult(result.Error);
    //}
}