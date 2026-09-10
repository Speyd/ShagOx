using ShagOxServer.Application.DTOs.Baskets;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Query;
public interface IBasketAttributeQueryService
    : IQueryService<BasketAttributeDto>
{
    Task<Result<PagedResult<BasketAttributeDto>>> GetByCategoryAsync(
        int categoryId,
        PaginationParams pagination);

    Task<Result<BasketAttributeDto>> GetByAttributeDefenitionAsync(
        int attributeDefenitionId);

    Task<Result<PagedResult<BasketAttributeDto>>> Search(
        BasketAttributeSearchFilter filter,
        PaginationParams pagination);
}