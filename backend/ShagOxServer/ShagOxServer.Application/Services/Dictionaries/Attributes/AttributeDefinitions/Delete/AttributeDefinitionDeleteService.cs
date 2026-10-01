using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Baskets;
using ShagOxServer.Application.Services.Caches.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Delete;
public class AttributeDefinitionDeleteService 
    : IAttributeDefinitionDeleteService
{
    private readonly IRepository<AttributeDefinition> _attributeRepository;
    private readonly AttributeDefinitionValidator _attributeValidator;
    private readonly IBasketAttributeQueryRepository _basketrepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDefinitionDeleteService> _logger;
    private readonly ICacheService _cache;



    public AttributeDefinitionDeleteService(
        IRepository<AttributeDefinition> attributeRepository,
        AttributeDefinitionValidator attributeValidator,
        IBasketAttributeQueryRepository basketrepository,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDefinitionDeleteService> logger,
        ICacheService cache)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _basketrepository = basketrepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _cache = cache;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var attribute = await _attributeValidator
            .GetByIdAsync(id);

        if (!attribute.IsSuccess)
            return Result<DeleteResponse>.Fail(attribute.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Delete(attribute.Value!);

            await _unitOfWork.CommitAsync();

            await AttributeDefinitionCache
                .InvalidateDeleteAsync(_cache, attribute.Value!);

            await BasketAttributeCache
                .InvalidateByAttributeDefinitionAsync(
                _cache,
                _basketrepository,
                id);
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete attribute definition. Id: {Id}",
               id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.AttributeDefinitionDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               attribute.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}