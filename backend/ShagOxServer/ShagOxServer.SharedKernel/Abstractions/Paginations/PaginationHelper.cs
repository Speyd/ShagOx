using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
        var sw = Stopwatch.StartNew();

        var totalCount = await query.CountAsync(cancellationToken);
        Console.WriteLine($"Count: {sw.ElapsedMilliseconds} ms");

        sw.Restart();

        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync(cancellationToken);

        Console.WriteLine($"Items: {sw.ElapsedMilliseconds} ms");

        sw.Restart();

        // Твой запрос Reviews

        Console.WriteLine($"Reviews: {sw.ElapsedMilliseconds} ms");

        return new PagedResult<T>(
            items,
            totalCount,
            pagination);
    }
}
