using ShagOxServer.Application.DTOs.Baskets.BasketAttributes;
using ShagOxServer.Application.DTOs.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.Core.Query;
public interface IBasketQueryService
    : IQueryService<BasketDto>
{
    Task<Result<BasketDto>> GetByUserAsync(
        int userId);

    Task<Result<PagedResult<BasketDto>>> Search(
        BasketSearchFilter filter,
        PaginationParams pagination);
}