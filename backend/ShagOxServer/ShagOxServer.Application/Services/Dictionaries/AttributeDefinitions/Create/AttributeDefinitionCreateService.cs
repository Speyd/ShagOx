using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Create;
public class AttributeDefinitionCreateService : IAttributeDefinitionCreateService
{
    private readonly IAttributeDefinitionRepository _attributeRepository;
    private readonly ICategoryExistsRepository _categoryExistsRepository;
    private readonly IUnitOfWork _unitOfWork;


    public AttributeDefinitionCreateService(
        IAttributeDefinitionRepository attributeRepository,
        ICategoryExistsRepository categoryExistsRepository,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _categoryExistsRepository = categoryExistsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AttributeDefinitionCreateResponse>> CreateAttributeDefinitionAsync(
        AttributeDefinitionCreateRequest request)
    {
        var categoryExists = await _categoryExistsRepository.ExistsIdAsync(request.CategoryId);
        if (!categoryExists)
            return Result<AttributeDefinitionCreateResponse>.NotFound("Category");

        var attribute = CreateAttributeDefinition(request);

        try
        {
            _attributeRepository.Add(attribute);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        var response = new AttributeDefinitionCreateResponse(
            attribute.Id,
            DateTime.UtcNow
        );

        return Result<AttributeDefinitionCreateResponse>.Success(response);
    }

    private AttributeDefinition CreateAttributeDefinition(
        AttributeDefinitionCreateRequest request)
    {
        return new AttributeDefinition
        {
            CategoryId = request.CategoryId,
            Key = request.Key,
            Type = request.Type,
            Required = request.Required,
            Min = request.Min,
            Max = request.Max,
        };
    }

}
