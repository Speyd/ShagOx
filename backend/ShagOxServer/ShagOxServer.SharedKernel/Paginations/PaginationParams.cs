namespace ShagOxServer.SharedKernel.Paginations;
public sealed record PaginationParams
(
    int page = 1,
    int pageSize = 20
);
