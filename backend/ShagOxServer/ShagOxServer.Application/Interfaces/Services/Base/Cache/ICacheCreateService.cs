using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Services.Base.Cache;
public interface ICacheCreateService<TDto, TFilter>
{
    public Task CreateCache(
        TDto dto);

    public Task CreatePagedCache(
        PaginationParams pagination,
        PagedResult<TDto> result);

    public Task CreateSearchCache(
        TFilter filter,
        PaginationParams pagination,
        PagedResult<TDto> result);
}
