using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Services.Base.Cache;
public interface ICacheQueryService<TDto, TFilter>
{
    public Task<TDto?> GetByCache(
        long id,
        EntityStatus status = EntityStatus.Active);

    public Task<PagedResult<TDto>?> GetByPagedCache(
        PaginationParams pagination,
        EntityStatus status = EntityStatus.Active);

    public Task<PagedResult<TDto>?> GetBySearchCache(
        TFilter filter,
        PaginationParams pagination,
        EntityStatus status = EntityStatus.Active);
}
