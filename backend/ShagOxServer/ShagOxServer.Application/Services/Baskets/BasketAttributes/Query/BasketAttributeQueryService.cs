using ShagOxServer.Application.DTOs.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Mapping;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Query;
public class BasketAttributeQueryService
    : IBasketAttributeQueryService
{
    private readonly IBasketAttributeQueryRepository _attributeQueryRepository;


    public BasketAttributeQueryService(
        IBasketAttributeQueryRepository attributeQueryRepository)
    {
        _attributeQueryRepository = attributeQueryRepository;
    }


    public async Task<Result<BasketAttributeDto>> GetByIdAsync(
        int id)
    {
        var attribute = await _attributeQueryRepository
            .GetByIdAsync(id);

        return attribute.ToResult(BasketAttributeMapper.ToDto);
    }

    public async Task<Result<BasketAttributeDto>> GetByAttributeDefenitionAsync(
        int attributeDefenitionId)
    {
        var attribute = await _attributeQueryRepository
            .GetByAttributeDefenitionAsync(attributeDefenitionId);

        return attribute.ToResult(BasketAttributeMapper.ToDto);
    }

    public async Task<Result<PagedResult<BasketAttributeDto>>> GetByCategoryAsync(
        int categoryId,
        PaginationParams pagination)
    {
        var attributes = await _attributeQueryRepository
            .GetByCategoryAsync(categoryId, pagination);

        return attributes.ToResultPaged(BasketAttributeMapper.ToDto);
    }

    public async Task<Result<PagedResult<BasketAttributeDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var attributes = await _attributeQueryRepository
            .GetPagedAsync(pagination);

        return attributes.ToResultPaged(BasketAttributeMapper.ToDto);
    }

    public async Task<Result<PagedResult<BasketAttributeDto>>> Search(
       BasketAttributeSearchFilter filter,
       PaginationParams pagination)
    {
        var attributes = await _attributeQueryRepository
            .Search(filter, pagination);

        return attributes.ToResultPaged(BasketAttributeMapper.ToDto);
    }
}