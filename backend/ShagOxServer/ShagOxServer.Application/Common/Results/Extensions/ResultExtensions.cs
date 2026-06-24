using Microsoft.AspNetCore.Mvc;

namespace ShagOxServer.Application.Common.Results.Extensions;
public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return new BadRequestObjectResult(result.Error);
    }
}