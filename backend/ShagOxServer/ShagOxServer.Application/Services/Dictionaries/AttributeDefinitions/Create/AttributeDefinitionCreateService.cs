using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Create;
public class AttributeDefinitionCreateService : IAttributeDefinitionCreateService
{
    private readonly IAttributeDefinitionRepository _attributeRepository;
    private readonly AttributeDefinitionValidator _validator;

    private readonly CategoryValidator _categoryValidator;

    private readonly IUnitOfWork _unitOfWork;


    public AttributeDefinitionCreateService(
        IAttributeDefinitionRepository attributeRepository,
        AttributeDefinitionValidator validator,
        CategoryValidator categoryValidator,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _validator = validator;
        _categoryValidator = categoryValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AttributeDefinitionCreateResponse>> CreateAttributeDefinitionAsync(
        AttributeDefinitionCreateRequest request)
    {
        var categoryExists = await _categoryValidator.ExistsCategoryValidator(request.CategoryId);
        if (!categoryExists.IsSuccess)
            return Result<AttributeDefinitionCreateResponse>.Fail(categoryExists.Error ?? "");

        var keyExists = await _validator.ExistsByKeyValidator(request.Key, request.CategoryId);
        if (!keyExists.IsSuccess)
            return Result<AttributeDefinitionCreateResponse>.Fail(keyExists.Error ?? "");

        var attribute = AttributeDefinitionCreater.CreateAttributeDefinition(request);

        await _unitOfWork.BeginTransactionAsync();

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
}