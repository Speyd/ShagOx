using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Validator;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Update;

public class AttributeDictionaryValueTranslationUpdateService
    : BaseTranslationUpdateService<AttributeDictionaryValue, AttributeDictionaryValueTranslation>,
    IAttributeDictionaryValueTranslationUpdateService
{
    private readonly IRepository<AttributeDictionaryValueTranslation> _attributeRepository;
    private readonly AttributeDictionaryValueTranslationValidator _attributeTranslationValidator;

    private readonly AttributeDictionaryValueTranslationInvalidationService _transInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDictionaryValueTranslationUpdateService> _logger;


    public AttributeDictionaryValueTranslationUpdateService(
        IRepository<AttributeDictionaryValueTranslation> attributeRepository,
        AttributeDictionaryValueTranslationValidator attributeTranslationValidator,
        AttributeDictionaryValueTranslationInvalidationService transInvalid,
        AttributeDictionaryValueValidator attributeValidator,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDictionaryValueTranslationUpdateService> logger
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
        AttributeDictionaryValueTranslationUpdateRequest request)
    {
        var attribute = await _attributeTranslationValidator
            .GetByIdAsync(attributeTranslationId);

        if (!attribute.IsSuccess)
            return Result<UpdateResponse>.Fail(attribute.Error);


        var validation = await
             ValidateUpdatesAsync(attribute.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = AttributeDictionaryValueTranslationUpdater
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
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete attribute dictionary value translation. Id: {Id}",
               attributeTranslationId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.AttributeDictionaryValueTranslationUpdateFailed);
        }

        await _transInvalid.InvalidateUpdateAsync(
            BaseTranslationCacheMapper.ToInfo(attribute.Value!));

        return Result<UpdateResponse>.Success(result);
    }
}