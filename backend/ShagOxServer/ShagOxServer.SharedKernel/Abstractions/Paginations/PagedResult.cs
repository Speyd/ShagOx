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
        PaginationParams pagination)
    {
        Items = items;

        Page = pagination.Page;
        PageSize = pagination.PageSize;

        TotalCount = items.Count;
        TotalPages = (int)Math.Ceiling(
            items.Count / (double)pagination.PageSize);
    }
}