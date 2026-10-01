using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Baskets;
using ShagOxServer.Application.Services.Caches.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Update.Validator;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Update;
public class AttributeDefinitionUpdateService 
    : IAttributeDefinitionUpdateService
{
    private readonly IRepository<AttributeDefinition> _attributeRepository;
    private readonly AttributeDefinitionValidator _attributeValidator;
    private readonly AttributeDefinitionUpdateValidator _attributeUpdateValidator;

    private readonly IBasketAttributeQueryRepository _basketrepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDefinitionUpdateService> _logger;
    private readonly ICacheService _cache;


    public AttributeDefinitionUpdateService(
        IRepository<AttributeDefinition> attributeRepository,
        AttributeDefinitionValidator attributeValidator,
        AttributeDefinitionUpdateValidator attributeUpdateValidator,
        IBasketAttributeQueryRepository basketrepository,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDefinitionUpdateService> logger,
        ICacheService cache)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _attributeUpdateValidator = attributeUpdateValidator;
        _basketrepository = basketrepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _cache = cache;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long attributeId,
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

            await AttributeDefinitionCache
                .InvalidateUpdateAsync(_cache, attribute.Value!);

            await BasketAttributeCache
                .InvalidateByAttributeDefinitionAsync(
                _cache,
                _basketrepository,
                attributeId);
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to update attribute definition. Id: {Id}",
               attributeId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.AttributeDefinitionUpdateFailed);
        }

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        (string key, long categoryId) changeValidator,
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