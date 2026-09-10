using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Mapping;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Query;
public class ProductTypeTranslationQueryService
    : IProductTypeTranslationQueryService
{
    private readonly IProductTypeTranslationQueryRepository _typeRepository;


    public ProductTypeTranslationQueryService(
        IProductTypeTranslationQueryRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }


    public async Task<Result<ProductTypeTranslationDto>> GetByIdAsync(
        int id)
    {
        var region = await _typeRepository
            .GetByIdAsync(id);

        return region.ToResult(ProductTypeTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<ProductTypeTranslationDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        var regions = await _typeRepository
            .GetPagedAsync(pagination, language);

        return regions.ToResultPaged(ProductTypeTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<ProductTypeTranslationDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var regions = await _typeRepository
            .GetPagedAsync(pagination);

        return regions.ToResultPaged(ProductTypeTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<ProductTypeTranslationDto>>> Search(
        ProductTypeTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        var regions = await _typeRepository
            .Search(filter, pagination);

        return regions.ToResultPaged(ProductTypeTranslationMapper.ToDto);
    }
}