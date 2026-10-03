using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Delete;
public class AttributeDefinitionTranslationDeleteService
    : IAttributeDefinitionTranslationDeleteService
{
    private readonly IRepository<AttributeDefinitionTranslation> _attributeRepository;
    private readonly AttributeDefinitionTranslationValidator _attributeValidator;

    private readonly AttributeDefinitionTranslationInvalidationService _transInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDefinitionTranslationDeleteService> _logger;


    public AttributeDefinitionTranslationDeleteService(
        IRepository<AttributeDefinitionTranslation> attributeRepository,
        AttributeDefinitionTranslationValidator attributeValidator,
         AttributeDefinitionTranslationInvalidationService transInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDefinitionTranslationDeleteService> logger)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _transInvalid = transInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var attributeTrans = await _attributeValidator
            .GetByIdAsync(id);

        if (!attributeTrans.IsSuccess)
            return Result<DeleteResponse>.Fail(attributeTrans.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Delete(attributeTrans.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete attribute definition translation. Id: {Id}",
               id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.AttributeDefinitionTranslationDeleteFailed);
        }

        await _transInvalid.InvalidateDeleteAsync(
            BaseTranslationCacheMapper.ToInfo(attributeTrans.Value!));

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               attributeTrans.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}