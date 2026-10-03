using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Delete;
public class AttributeDictionaryValueTranslationDeleteService
    : IAttributeDictionaryValueTranslationDeleteService
{
    private readonly IRepository<AttributeDictionaryValueTranslation> _attributeRepository;
    private readonly AttributeDictionaryValueTranslationValidator _attributeValidator;

    private readonly AttributeDictionaryValueTranslationInvalidationService _transInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDictionaryValueTranslationDeleteService> _logger;


    public AttributeDictionaryValueTranslationDeleteService(
        IRepository<AttributeDictionaryValueTranslation> attributeRepository,
        AttributeDictionaryValueTranslationValidator attributeValidator,
        AttributeDictionaryValueTranslationInvalidationService transInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDictionaryValueTranslationDeleteService> logger)
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
        var attribute = await _attributeValidator
            .GetByIdAsync(id);

        if (!attribute.IsSuccess)
            return Result<DeleteResponse>.Fail(attribute.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Delete(attribute.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete attribute dictionary value translation. " + 
               "Id: {Id}",
               id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.AttributeDictionaryValueTranslationDeleteFailed);
        }

        await _transInvalid.InvalidateDeleteAsync(
            BaseTranslationCacheMapper.ToInfo(attribute.Value!));

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               attribute.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}