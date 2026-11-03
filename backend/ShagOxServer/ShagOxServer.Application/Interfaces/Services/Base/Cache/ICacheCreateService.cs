using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Services.Base.Cache;
public interface ICacheCreateService<TDto, TFilter>
{
    public Task CreateCache(
        TDto dto,
        EntityStatus status = EntityStatus.Active);

    public Task CreatePagedCache(
        PaginationParams pagination,
        PagedResult<TDto> result,
        EntityStatus status = EntityStatus.Active);

    public Task CreateSearchCache(
        TFilter filter,
        PaginationParams pagination,
        PagedResult<TDto> result,
        EntityStatus status = EntityStatus.Active);
}
