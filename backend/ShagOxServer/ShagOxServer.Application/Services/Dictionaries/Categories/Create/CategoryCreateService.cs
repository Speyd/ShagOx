using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Create;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;


namespace ShagOxServer.Application.Services.Dictionaries.Categories.Create;
public class CategoryCreateService : ICategoryCreateService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAttributeDefinitionQueryRepository _attributeRepository;
    private readonly IAdvertisementQueryRepository _advertisementRepository;


    public CategoryCreateService(
        ICategoryRepository categoryRepository,
        IAttributeDefinitionQueryRepository attributeRepository,
        IAdvertisementQueryRepository advertisementRepository)
    {
        _categoryRepository = categoryRepository;
        _attributeRepository = attributeRepository;
        _advertisementRepository = advertisementRepository;
    }

    public async Task<Result<CategoryCreateResponse>> CreateCategoryAsync(
        CategoryCreateRequest request)
    {
        var attributes = await _attributeRepository.GetByIdsAsync(request.Attributes);

        if (request.Attributes.Count != 0 &&
            attributes.Count != request.Attributes.Count)
        {
            var missing = request.Attributes.Except(attributes.Select(x => x.Id));
            return Result<CategoryCreateResponse>
                .NotFound($"Attributes not found: {string.Join(", ", missing)}");
        }


        var advertisements = await _advertisementRepository
            .GetByIdsAsync(request.Advertisements);

        if (request.Advertisements.Count != 0 && 
            advertisements.Count != request.Advertisements.Count)
        {
            var missing = request.Advertisements.Except(advertisements.Select(x => x.Id));
            return Result<CategoryCreateResponse>
                .NotFound($"Advertisements not found: {string.Join(", ", missing)}");
        }

        var category = CreateCategory(attributes, advertisements, request);
        await _categoryRepository.AddAsync(category);

        var response = new CategoryCreateResponse(
            category.Id,
            DateTime.UtcNow
        );

        return Result<CategoryCreateResponse>.Success(response);
    }

    private Category CreateCategory(
        List<AttributeDefinition> attributes,
        List<Advertisement>  advertisements,
        CategoryCreateRequest request)
    {
        return new Category
        {
            Name = request.Name,
            ProductType = request.ProductType,
            Attributes = attributes,
            Advertisements = advertisements
        };
    }
}
