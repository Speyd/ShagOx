using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Update;
public class AttributeDefinitionUpdateService 
    : IAttributeDefinitionUpdateService
{
    private readonly IRepository<AttributeDefinition> _attributeRepository;
    private readonly AttributeDefinitionValidator _attributeValidator;
    private readonly AttributeDefinitionUpdateValidator _attributeUpdateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public AttributeDefinitionUpdateService(
        IRepository<AttributeDefinition> attributeRepository,
        AttributeDefinitionValidator attributeValidator,
        AttributeDefinitionUpdateValidator attributeUpdateValidator,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _attributeUpdateValidator = attributeUpdateValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int attributeId,
        AttributeDefinitionUpdateRequest request)
    {
        var attribute = await _attributeValidator.GetByIdAsync(attributeId);
        if (!attribute.IsSuccess)
            return Result<UpdateResponse>.Fail(attribute.Error);

        var changeValidator = _attributeUpdateValidator
            .HasChangesValidator(attribute.Value!, request);

        if (!changeValidator.IsSuccess)
        {
            return Result<UpdateResponse>.Success(
                new UpdateResponse(
                    0,
                    DateTime.UtcNow
            ));
        }

        var validation = await
             ValidateUpdatesAsync(changeValidator.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = AttributeDefinitionUpdater
            .ApplyUpdates(attribute.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Update(attribute.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        (string key, int categoryId) changeValidator,
        AttributeDefinitionUpdateRequest request)
    {
        var existsValidator = await _attributeValidator
            .NotExistsByKeyAsync(
                changeValidator.key,
                changeValidator.categoryId
        );

        if (!existsValidator.IsSuccess)
            return Result<bool>.Fail(existsValidator.Error);

        return Result<bool>.Success(true);
    }
}