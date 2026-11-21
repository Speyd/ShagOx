using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.Domain.Filters.LineItems.LineItemAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Query;
public interface ILineItemAttributeQueryService
    : IQueryService<LineItemAttributeDto,
        LineItemAttribute,
        LineItemAttributeSearchFilter>
{
    Task<Result<PagedResult<LineItemAttributeDto>>> GetByCategoryAsync(
        long categoryId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<LineItemAttributeDto>> GetByAttributeDefinitionAsync(
        long attributeDefinitionId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
