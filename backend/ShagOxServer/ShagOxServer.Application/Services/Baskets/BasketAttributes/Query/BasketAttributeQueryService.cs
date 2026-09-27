using ShagOxServer.Application.DTOs.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Mapping;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Query;
public class BasketAttributeQueryService
    : BaseQueryService<
        BasketAttributeDto,
        BasketAttribute,
        BasketAttributeSearchFilter
        >,
    IBasketAttributeQueryService
{
    private readonly IBasketAttributeQueryRepository _attributeQueryRepository;


    public BasketAttributeQueryService(
        IBasketAttributeQueryRepository attributeQueryRepository
    )
        : base(attributeQueryRepository)
    {
        _attributeQueryRepository = attributeQueryRepository;
    }


    protected override BasketAttributeDto ApplyMapper(
        BasketAttribute entity)
    {
        return BasketAttributeMapper.ToDto(entity);
    }

    public async Task<Result<BasketAttributeDto>> GetByAttributeDefenitionAsync(
        long attributeDefenitionId)
    {
        var attribute = await _attributeQueryRepository
            .GetByAttributeDefenitionAsync(attributeDefenitionId);

        return attribute.ToResult(ApplyMapper);
    }

    public async Task<Result<PagedResult<BasketAttributeDto>>> GetByCategoryAsync(
        long categoryId,
        PaginationParams pagination)
    {
        var attributes = await _attributeQueryRepository
            .GetByCategoryAsync(categoryId, pagination);

        return attributes.ToResultPaged(ApplyMapper);
    }
}