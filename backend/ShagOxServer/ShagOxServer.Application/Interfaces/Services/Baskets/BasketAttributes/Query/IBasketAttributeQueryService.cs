using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Query;
public interface IBasketAttributeQueryService
    : IQueryService<BasketAttributeDto,
        BasketAttribute,
        BasketAttributeSearchFilter>
{
    Task<Result<PagedResult<BasketAttributeDto>>> GetByCategoryAsync(
        long categoryId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    Task<Result<BasketAttributeDto>> GetByAttributeDefinitionAsync(
        long attributeDefinitionId,
        CancellationToken cancellationToken = default);
}
