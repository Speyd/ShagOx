using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using Twilio.TwiML.Voice;

namespace ShagOxServer.Application.Interfaces.Services.Base.Query;
public interface IQueryOperations<TDto, TFilter>
    where TDto : BaseDto
    where TFilter : BaseFilter
{
    Task<Result<long>> GetCountAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<TDto>> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<PagedResult<TDto>>> GetPagedAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<PagedResult<TDto>>> SearchAsync(
        TFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
