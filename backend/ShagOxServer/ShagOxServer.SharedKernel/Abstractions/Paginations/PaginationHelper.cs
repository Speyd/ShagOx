using Microsoft.EntityFrameworkCore;

namespace ShagOxServer.SharedKernel.Abstractions.Paginations;
public static class PaginationHelper
{
    public static IQueryable<T> WithPagination<T>(
        this IQueryable<T> collection,
        PaginationParams pagination)
    {
        return collection
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize);
    }

    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
       this IQueryable<T> query,
       PaginationParams pagination)
    {
        var totalCount = await query.CountAsync();

        var items = await query
            .WithPagination(pagination)
            .ToListAsync();

        return new PagedResult<T>(
            items,
            totalCount,
            pagination);
    }
}