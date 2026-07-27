namespace ShagOxServer.SharedKernel.Abstractions.Paginations;
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];

    public int Page { get; init; }

    public int PageSize { get; init; }

    public int TotalCount { get; init; }

    public int TotalPages { get; init; }

    public PagedResult(
        IReadOnlyList<T> items,
        int totalCount,
        PaginationParams pagination)
    {
        Items = items;

        Page = pagination.Page;
        PageSize = pagination.PageSize;

        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(
            totalCount / (double)pagination.PageSize);
    }
}