using Microsoft.AspNetCore.Mvc;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
public static class ActionResultExtensions
{
    public static IActionResult ToActionResult<T>(
        this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return new BadRequestObjectResult(result.Error);
    }
    public static IActionResult ToActionListResult<T>(
        this Result<IEnumerable<T>> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return new BadRequestObjectResult(result.Error);
    }
}
