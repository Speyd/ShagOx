using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Query;
public class CategoryQueryService 
    : BaseQueryService<
        CategoryDto,
        Category,
        CategorySearchFilter
        >,
    ICategoryQueryService
{
    private readonly ICategoryQueryRepository _categoryQueryRepository;


    public CategoryQueryService(
        ICategoryQueryRepository categoryQueryRepository
    )
        : base(categoryQueryRepository)
    {
        _categoryQueryRepository = categoryQueryRepository;
    }


    protected override async Task<CategoryDto> ApplyMapperAsync(
        Category entity)
    {
        return CategoryMapper.ToDto(entity);
    }

    public async Task<Result<PagedResult<CategoryDto>>> GetByProductTypeAsync(
        long productTypeId,
        PaginationParams pagination)
    {
        var categories = await _categoryQueryRepository
            .GetByProductTypeAsync(productTypeId, pagination);

        return await categories.ToResultPagedAsync(ApplyMapperAsync);
    }
}