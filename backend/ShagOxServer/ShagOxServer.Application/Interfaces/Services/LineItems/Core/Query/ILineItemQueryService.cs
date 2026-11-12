using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Items;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.LineItems.Core.Query;
public interface ILineItemQueryService<TDto, TEntity, TFilter>
    : IQueryService<TDto, TEntity, TFilter>
    where TDto : BaseDto
    where TEntity : LineItem
    where TFilter : BaseFilter
{
    Task<Result<PagedResult<TDto>>> GetPagedAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<PagedResult<TDto>>> GetByParentAsync(
        long parentId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
