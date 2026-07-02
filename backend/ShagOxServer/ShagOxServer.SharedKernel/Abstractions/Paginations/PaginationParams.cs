namespace ShagOxServer.SharedKernel.Abstractions.Paginations;
public sealed record PaginationParams
(
    int Page = 1,
    int PageSize = 20
);
