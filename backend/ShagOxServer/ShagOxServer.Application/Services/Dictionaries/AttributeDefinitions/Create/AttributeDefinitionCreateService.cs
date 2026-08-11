using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Create;
public class AttributeDefinitionCreateService
    : IAttributeDefinitionCreateService
{
    private readonly IRepository<AttributeDefinition> _attributeRepository;
    private readonly AttributeDefinitionValidator _attributeValidator;

    private readonly CategoryValidator _categoryValidator;

    private readonly IUnitOfWork _unitOfWork;


    public AttributeDefinitionCreateService(
        IRepository<AttributeDefinition> attributeRepository,
        AttributeDefinitionValidator attributeValidator,
        CategoryValidator categoryValidator,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _categoryValidator = categoryValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AttributeDefinitionCreateRequest request)
    {
        var categoryExists = await _categoryValidator
            .ExistsByIdAsync(request.CategoryId);

        if (!categoryExists.IsSuccess)
            return Result<CreateResponse>.Fail(categoryExists.Error);


        var keyExists = await _attributeValidator
            .NotExistsByKeyAsync(request.Key, request.CategoryId);

        if (!keyExists.IsSuccess)
            return Result<CreateResponse>.Fail(keyExists.Error);


        var attribute = AttributeDefinitionCreater.Create(request);

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

        return Result<CreateResponse>.Success(
            new CreateResponse(
                attribute.Id,
                DateTime.UtcNow
        ));
    }
}