using Npgsql;
using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Application.Interfaces.Dictionaries.Categories.Update;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.Categories;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Update;
public class CategoryUpdateService : ICategoryUpdateService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAttributeDefinitionQueryRepository _attributeRepository;
    private readonly IAdvertisementQueryRepository _advertisementRepository;


    public CategoryUpdateService(
        ICategoryRepository categoryRepository,
        IAttributeDefinitionQueryRepository attributeRepository,
        IAdvertisementQueryRepository advertisementRepository)
    {
        _categoryRepository = categoryRepository;
        _attributeRepository = attributeRepository;
        _advertisementRepository = advertisementRepository;
    }

    public async Task<Result<CategoryUpdateResponse>> UpdateCategoryAsync(
        int categoryId,
        CategoryUpdateRequest request)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if(category is null)
            return Result<CategoryUpdateResponse>.NotFound("Category");

        var attributes = new List<AttributeDefinition>();
        if (ValidateAttributes(category, request) && request.Attributes is not null)
            attributes = await _attributeRepository.GetByIdsAsync(request.Attributes);

        var advertisements = new List<Advertisement>();
        if (ValidateAdvertisements(category, request) && request.Advertisements is not null)
            advertisements = await _advertisementRepository.GetByIdsAsync(request.Advertisements);
    
        var updatedCount = ApplyUpdates(category, attributes, advertisements, request);
        var result = new CategoryUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<CategoryUpdateResponse>.Success(result);

        await _categoryRepository.UpdateAsync(category);

        return Result<CategoryUpdateResponse>.Success(result);
    }

    private static int ApplyUpdates(
        Category category,
        List<AttributeDefinition> Attributes,
        List<Advertisement> Advertisements,
        CategoryUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Name is not null)
        {
            category.Name = request.Name;
            countUpdated++;
        }

        if (request.ProductType is not null)
        {
            category.ProductType = request.ProductType.Value;
            countUpdated++;
        }

        if (Attributes is not null &&
            !Attributes.Any())
        {
            category.Attributes = Attributes;
            countUpdated++;
        }

        if (Advertisements is not null &&
            !Advertisements.Any())
        {
            category.Advertisements = Advertisements;
            countUpdated++;
        }

        return countUpdated;
    }

    private bool ValidateAttributes(
        Category category,
        CategoryUpdateRequest request)
    {
        if(request.Attributes is null)
            return false;

        return !request.Attributes
            .Except(category.Attributes.Select(x => x.Id))
            .Any();
    }

    private bool ValidateAdvertisements(
        Category category,
        CategoryUpdateRequest request)
    {
        if (request.Advertisements is null)
            return false;

        return !request.Advertisements
            .Except(category.Advertisements.Select(x => x.Id))
            .Any();
    }
}
