using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update;
public class AttributeDefinitionUpdateService : IAttributeDefinitionUpdateService
{
    private readonly IAttributeDefinitionRepository _attributeRepository;
    private readonly AttributeDefinitionValidator _attributeValidator;
    private readonly AttributeDefinitionUpdateValidator _attributeUpdateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public AttributeDefinitionUpdateService(
        IAttributeDefinitionRepository attributeRepository,
        AttributeDefinitionValidator attributeValidator,
        AttributeDefinitionUpdateValidator attributeUpdateValidator,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _attributeUpdateValidator = attributeUpdateValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<AttributeDefinitionUpdateResponse>> UpdateAttributeDefinitionAsync(
        int attributeId,
        AttributeDefinitionUpdateRequest request)
    {
        var attribute = await _attributeValidator.GetByIdAsync(attributeId);
        if (!attribute.IsSuccess)
            return Result<AttributeDefinitionUpdateResponse>.Fail(attribute.Error ?? "");

        var changeValidator = _attributeUpdateValidator
            .HasChangesValidator(attribute.Value!, request);

        if (!changeValidator.IsSuccess)
        {
            return Result<AttributeDefinitionUpdateResponse>.Success(
                new AttributeDefinitionUpdateResponse(
                DateTime.UtcNow,
                0
            ));
        }

        var existsValidator = await _attributeValidator.NotExistsByKeyAsync(
          changeValidator.Value!.key,
          changeValidator.Value!.categoryId
        );

        if (!existsValidator.IsSuccess)
            return Result<AttributeDefinitionUpdateResponse>.Fail(existsValidator.Error ?? "");


        var updatedCount = AttributeDefinitionUpdater.ApplyUpdates(attribute.Value!, request);
        var result = new AttributeDefinitionUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<AttributeDefinitionUpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Add(attribute.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<AttributeDefinitionUpdateResponse>.Success(result);
    }
}