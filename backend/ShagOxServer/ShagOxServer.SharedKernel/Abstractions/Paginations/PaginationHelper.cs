using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ShagOxServer.SharedKernel.Abstractions.Paginations;
public static class PaginationHelper
{
    public static IQueryable<T> WithPagination<T>(
        this IQueryable<T> collection,
        PaginationParams pagination,
        Expression<Func<T, object>> orderBy,
        bool descending = false)
    {
        var orderedQuery = descending
            ? collection.OrderByDescending(orderBy)
            : collection.OrderBy(orderBy);

        return orderedQuery
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize);
    }

    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
       this IQueryable<T> query,
       PaginationParams pagination,
        Expression<Func<T, object>> orderBy,
        CancellationToken cancellationToken = default,
        bool descending = false)
    {


        var totalCount = await query
            .CountAsync(cancellationToken);

        var firstItems = await query.Take(20).ToListAsync();

        Console.WriteLine($"Count: {totalCount}");
        Console.WriteLine($"First items: {firstItems.Count}");

        var items = await query
            .WithPagination(pagination,
                orderBy, descending)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>(
            items,
            totalCount,
            pagination);
    }
}
