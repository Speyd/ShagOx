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
}