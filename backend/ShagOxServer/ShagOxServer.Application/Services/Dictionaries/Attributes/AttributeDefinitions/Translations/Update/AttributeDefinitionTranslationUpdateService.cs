using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Validator;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Update;
public class AttributeDefinitionTranslationUpdateService
    : BaseTranslationUpdateService<AttributeDefinition,
        AttributeDefinitionTranslation>,
    IAttributeDefinitionTranslationUpdateService
{
    private readonly IRepository<AttributeDefinitionTranslation> _attributeRepository;
    private readonly AttributeDefinitionTranslationValidator _attributeTranslationValidator;

    private readonly AttributeDefinitionTranslationInvalidationService _transInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDefinitionTranslationUpdateService> _logger;


    public AttributeDefinitionTranslationUpdateService(
        IRepository<AttributeDefinitionTranslation> attributeRepository,
        AttributeDefinitionTranslationValidator attributeTranslationValidator,
        AttributeDefinitionValidator attributeValidator,
        AttributeDefinitionTranslationInvalidationService transInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDefinitionTranslationUpdateService> logger
    ) : base(attributeValidator, attributeTranslationValidator)
    {
        _attributeRepository = attributeRepository;
        _attributeTranslationValidator = attributeTranslationValidator;
        _transInvalid = transInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long attributeTranslationId,
        AttributeDefinitionTranslationUpdateRequest request)
    {
        var attributeTrans = await _attributeTranslationValidator
            .GetByIdAsync(attributeTranslationId);

        if (!attributeTrans.IsSuccess)
            return Result<UpdateResponse>.Fail(attributeTrans.Error);


        var validation = await
             ValidateUpdatesAsync(attributeTrans.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = AttributeDefinitionTranslationUpdater
            .ApplyUpdates(attributeTrans.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _attributeRepository.Update(attributeTrans.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete attribute definition translation. Id: {Id}",
               attributeTranslationId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.AttributeDefinitionTranslationUpdateFailed);
        }

        await _transInvalid.InvalidateUpdateAsync(
            BaseTranslationCacheMapper.ToInfo(attributeTrans.Value!));

        return Result<UpdateResponse>.Success(result);
    }
}