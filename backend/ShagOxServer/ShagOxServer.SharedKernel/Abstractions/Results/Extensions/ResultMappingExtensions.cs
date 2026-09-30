using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Resources.Results;

namespace ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
public static class ResultMappingExtensions
{
    public static Result<TDto> ToResult<T, TDto>(
        this T? entity,
        Func<T, TDto> map,
        string? errorMessage = null)
        where T : class
    {
        if (errorMessage is null)
            errorMessage = ResultResources.EntityNotFound;

        if (entity is null)
            return Result<TDto>.Fail(errorMessage);

        return Result<TDto>.Success(map(entity));
    }

    public static async Task<Result<TDto>> ToResultAsync<T, TDto>(
         this T? entity,
         Func<T, Task<TDto>> map,
         string? errorMessage = null)
         where T : class
    {
        if (errorMessage is null)
            errorMessage = ResultResources.EntityNotFound;

        if (entity is null)
            return Result<TDto>.Fail(errorMessage);

        return Result<TDto>.Success(await map(entity));
    }

    public static Result<List<TDto>> ToResultList<T, TDto>(
        this IEnumerable<T> entities,
        Func<T, TDto> map,
        string? errorMessage = null)
        where T : class
    {
        if (errorMessage is null)
            errorMessage = ResultResources.EntityNotFound;

        if (entities is null)
        {
            return Result<List<TDto>>
                .Fail(errorMessage);
        }

        return Result<List<TDto>>.Success(
            entities.Select(map).ToList()
        );
    }

    public static async Task<Result<List<TDto>>> ToResultListAsync<T, TDto>(
       this IEnumerable<T> entities,
       Func<T, Task<TDto>> map,
       string? errorMessage = null)
       where T : class
    {
        if (errorMessage is null)
            errorMessage = ResultResources.EntityNotFound;

        if (entities is null)
        {
            return Result<List<TDto>>
                .Fail(errorMessage);
        }

        var result = new List<TDto>();

        foreach (var entity in entities)
        {
            result.Add(await map(entity));
        }

        return Result<List<TDto>>.Success(result);
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

    public static async Task<Result<PagedResult<TDto>>> ToResultPagedAsync<T, TDto>(
        this PagedResult<T> result,
        Func<T, Task<TDto>> map)
        where T : class
    {
        var items = new List<TDto>();

        foreach (var item in result.Items)
        {
            items.Add(await map(item));
        }

        return Result<PagedResult<TDto>>.Success(
            new PagedResult<TDto>(
                items,
                result.TotalCount,
                new PaginationParams(
                    result.Page,
                    result.PageSize))
        );
    }
}