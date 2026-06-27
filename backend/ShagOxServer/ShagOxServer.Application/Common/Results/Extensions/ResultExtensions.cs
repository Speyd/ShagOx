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

    public static Result<TDto> ToResult<T, TDto>(
       this T? entity,
       Func<T, TDto> map,
       string errorMessage = "Not found")
       where T : class
    {
        if (entity is null)
            return Result<TDto>.Fail(errorMessage);

        return Result<TDto>.Success(map(entity));
    }

    public static Result<List<TDto>> ToResultList<T, TDto>(
      this IEnumerable<T> entities,
      Func<T, TDto> map,
      string errorMessage = "Not found")
      where T : class
    {
        if (entities is null || !entities.Any())
            return Result<List<TDto>>.Fail(errorMessage);

        return Result<List<TDto>>.Success(
            entities.Select(map).ToList()
        );
    }
}