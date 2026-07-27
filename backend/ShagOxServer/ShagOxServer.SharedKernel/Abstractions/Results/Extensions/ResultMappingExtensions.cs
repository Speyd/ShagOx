using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
public static class ResultMappingExtensions
{
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
        if (entities is null)
            return Result<List<TDto>>.Fail(errorMessage);

        return Result<List<TDto>>.Success(
            entities.Select(map).ToList()
        );
    }

    public static Result<PagedResult<TDto>> ToResultPaged<T, TDto>(
        this PagedResult<T> result,
        Func<T, TDto> map)
        where T : class
    {
        return Result<PagedResult<TDto>>.Success(
            new PagedResult<TDto>(
                result.Items.Select(map).ToList(),
                result.TotalCount,
                new PaginationParams(result.Page, result.PageSize))
        );
    }
}