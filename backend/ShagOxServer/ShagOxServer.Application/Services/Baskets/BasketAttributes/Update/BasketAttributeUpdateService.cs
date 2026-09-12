using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Update;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Update.Validator;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Update;
public class BasketAttributeUpdateService
    : IBasketAttributeUpdateService
{
    private readonly IRepository<BasketAttribute> _attributeRepository;
    private readonly BasketAttributeValidator _attributeValidator;
    private readonly BasketAttributeUpdateValidator _attributeUpdateValidator;

    private readonly IRepository<AttributeDefinition> _attributeDefinitionValidator;

    private readonly IUnitOfWork _unitOfWork;


    public BasketAttributeUpdateService(
        IRepository<BasketAttribute> attributeRepository,
        BasketAttributeValidator attributeValidator,
        BasketAttributeUpdateValidator attributeUpdateValidator,
        IRepository<AttributeDefinition> attributeDefinitionValidator,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _attributeUpdateValidator = attributeUpdateValidator;
        _attributeDefinitionValidator = attributeDefinitionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int attributeId,
        BasketAttributeUpdateRequest request)
    {
        var attribute = await _attributeValidator
            .GetByIdAsync(attributeId);

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


        var updatedCount = BasketAttributeUpdater
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
        (int order, int attributeDefinitionId) changeValidator,
        BasketAttributeUpdateRequest request)
    {
        var attributeDef = await _attributeDefinitionValidator
            .GetByIdAsync(changeValidator.attributeDefinitionId);

        if (attributeDef is null)
            return Result<bool>.NotFound(typeof(AttributeDefinition));

        var existsValidator = await _attributeValidator
            .NotExistsAsync(attributeDef, changeValidator.order);

        if (!existsValidator.IsSuccess)
            return Result<bool>.Fail(existsValidator.Error);

        return Result<bool>.Success(true);
    }
}