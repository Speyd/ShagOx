using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Services.Base.Cache;
public interface ICacheQueryService<TDto, TFilter>
{
    public Task<TDto?> GetByCache(
        long id);

    public Task<PagedResult<TDto>?> GetByPagedCache(
        PaginationParams pagination);

    public Task<PagedResult<TDto>?> GetBySearchCache(
        TFilter filter,
        PaginationParams pagination);
}
