using ShagOxServer.Application.DTOs.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.Core.Query;
public interface IBasketQueryService
    : IQueryService<BasketDto, BasketSearchFilter>
{
    Task<Result<BasketDto>> GetByUserAsync(
        long userId);
}